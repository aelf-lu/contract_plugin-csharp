using AElf;
using Google.Protobuf.Reflection;

namespace ContractGenerator;

public partial class Generator : GeneratorBase
{
    private const string ServiceFieldName = "__ServiceName";
    private GeneratorOptions _options;
    private ServiceDescriptor _serviceDescriptor;

    public Generator(ServiceDescriptor serviceDescriptor, GeneratorOptions options, IndentPrinter? printer = null):base(printer)
    {
        _serviceDescriptor = serviceDescriptor;
        _options = options;
    }

    /// <summary>
    ///     Generate will produce a chunk of C# code that serves as the container class of the AElf Contract.
    /// </summary>
    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L612
    public string? Generate()
    {
        // GenerateDocCommentBody(serviceDescriptor,)
        indentPrinter.PrintLine($"{AccessLevel} static partial class {ServiceContainerClassName}");
        indentPrinter.PrintLine("{");
        indentPrinter.Indent();
        indentPrinter.PrintLine($"""static readonly string {ServiceFieldName} = "{_serviceDescriptor.FullName}";""");

        Marshallers();
        indentPrinter.PrintLine();
        Methods();
        indentPrinter.PrintLine();
        Descriptors();

        if (_options.GenerateContract)
        {
            indentPrinter.PrintLine();
            GenerateContractBaseClass();
            indentPrinter.PrintLine();
            GenerateBindServiceMethod();
        }

        if (_options.GenerateStub) GenerateStubClass();

        if (_options.GenerateReference) GenerateReferenceClass();
        indentPrinter.Outdent();
        indentPrinter.PrintLine("}");
        return indentPrinter.ToString();
    }

    private void GenerateAllServiceDescriptorsProperty()
    {
        indentPrinter.PrintLine(
            "public static global::System.Collections.Generic.IReadOnlyList<global::Google.Protobuf.Reflection.ServiceDescriptor> Descriptors"
        );
        indentPrinter.PrintLine("{");
        {
            indentPrinter.Indent();
            indentPrinter.PrintLine("get");
            indentPrinter.PrintLine("{");
            {
                indentPrinter.Indent();
                indentPrinter.PrintLine(
                    "return new global::System.Collections.Generic.List<global::Google.Protobuf.Reflection.ServiceDescriptor>()");
                indentPrinter.PrintLine("{");
                {
                    indentPrinter.Indent();
                    var services = _serviceDescriptor.GetFullService();
                    foreach (var service in services)
                    {
                        var index = service.Index.ToString();
                        indentPrinter.PrintLine(
                            $"{ProtoUtils.GetReflectionClassName(service.File)}.Descriptor.Services[{index}],");
                    }

                    indentPrinter.Outdent();
                }
                indentPrinter.PrintLine("};");
                indentPrinter.Outdent();
            }
            indentPrinter.PrintLine("}");
            indentPrinter.Outdent();
        }
        indentPrinter.PrintLine("}");
    }

    private string GetServerClassName()
    {
        return _serviceDescriptor.Name + "Base";
    }

    /// <summary>
    ///     Generates a section of instantiated aelf Marshallers as part of the contract
    /// </summary>
    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L332
    private void Marshallers()
    {
        indentPrinter.PrintLine("#region Marshallers");
        var usedMessages = GetUsedMessages();
        foreach (var usedMessage in usedMessages)
        {
            var fieldName = GetMarshallerFieldName(usedMessage);
            var t = ProtoUtils.GetClassName(usedMessage);
            indentPrinter.PrintLine(
                $"static readonly aelf::Marshaller<{t}> {fieldName} = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), {t}.Parser.ParseFrom);");
        }

        indentPrinter.PrintLine("#endregion");
    }


    private void GenerateServiceDescriptorProperty()
    {
        indentPrinter.PrintLine(
            "public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor");
        indentPrinter.PrintLine("{");
        indentPrinter.PrintLine(
            $"  get {{ return {ProtoUtils.GetReflectionClassName(_serviceDescriptor.File)}.Descriptor.Services[{_serviceDescriptor.Index}]; }}");
        indentPrinter.PrintLine("}");
    }


    /// <summary>
    ///     GetMarshallerFieldName formats and returns a marshaller-fieldname based on the original C++ logic
    ///     found here
    ///     https://github.com/AElfProject/contract-plugin/blob/de625fcb79f83603e29d201c8488f101b40f573c/src/contract_csharp_generator.cc#L242
    /// </summary>
    private static string GetMarshallerFieldName(IDescriptor message)
    {
        var msgFullName = message.FullName;
        return "__Marshaller_" + msgFullName.Replace(".", "_");
    }

    //TODO Implement https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L222
    private List<MethodDescriptor> GetFullMethod()
    {
        return _serviceDescriptor.GetFullService().SelectMany(serviceItem => serviceItem.Methods).ToList();
    }

    private static string GetMethodFieldName(MethodDescriptor method)
    {
        return "__Method_" + method.Name;
    }

    private static string GetCSharpMethodType(MethodDescriptor method)
    {
        return IsViewOnlyMethod(method) ? "aelf::MethodType.View" : "aelf::MethodType.Action";
    }


    private static bool IsViewOnlyMethod(MethodDescriptor method)
    {
        return method.GetOptions().GetExtension(OptionsExtensions.IsView);
    }

    /// <summary>
    ///     GetUsedMessages extracts messages from Proto ServiceDescriptor based on the original C++ logic
    ///     found here
    ///     https://github.com/AElfProject/contract-plugin/blob/de625fcb79f83603e29d201c8488f101b40f573c/src/contract_csharp_generator.cc#L312
    /// </summary>
    private List<IDescriptor> GetUsedMessages()
    {
        var descriptorSet = new HashSet<IDescriptor>();
        var result = new List<IDescriptor>();

        var methods = GetFullMethod();
        foreach (var method in methods)
        {
            if (!descriptorSet.Contains(method.InputType))
            {
                descriptorSet.Add(method.InputType);
                result.Add(method.InputType);
            }

            if (descriptorSet.Contains(method.OutputType)) continue;
            descriptorSet.Add(method.OutputType);
            result.Add(method.OutputType);
        }

        return result;
    }


    private enum MethodType
    {
        MethodtypeNoStreaming,
        MethodtypeClientStreaming,
        MethodtypeServerStreaming,
        MethodtypeBidiStreaming
    }

    private string AccessLevel => _options.InternalAccess ? "internal" : "public";

    //TODO Implement https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L115
    private string ServiceContainerClassName => $"{_serviceDescriptor.Name}Container";
}
