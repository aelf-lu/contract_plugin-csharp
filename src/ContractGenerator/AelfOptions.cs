// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: aelf/options.proto
// </auto-generated>
// This was generated via protoc using aelf/options.proto : protoc --proto_path=protos_test --csharp_out=build/gen --csharp_opt=file_extension=.g.cs,base_namespace=AElf,internal_access protos_test/aelf/options.proto
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AElf {

  /// <summary>Holder for reflection information generated from aelf/options.proto</summary>
  internal static partial class OptionsReflection {

    #region Descriptor
    /// <summary>File descriptor for aelf/options.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static OptionsReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJhZWxmL29wdGlvbnMucHJvdG8SBGFlbGYaIGdvb2dsZS9wcm90b2J1Zi9k",
            "ZXNjcmlwdG9yLnByb3RvOjAKCGlkZW50aXR5EhwuZ29vZ2xlLnByb3RvYnVm",
            "LkZpbGVPcHRpb25zGKHCHiABKAk6LwoEYmFzZRIfLmdvb2dsZS5wcm90b2J1",
            "Zi5TZXJ2aWNlT3B0aW9ucxip6R4gAygJOjcKDGNzaGFycF9zdGF0ZRIfLmdv",
            "b2dsZS5wcm90b2J1Zi5TZXJ2aWNlT3B0aW9ucxjG6R4gASgJOjEKB2lzX3Zp",
            "ZXcSHi5nb29nbGUucHJvdG9idWYuTWV0aG9kT3B0aW9ucxiR8R4gASgIOjMK",
            "CGlzX2V2ZW50Eh8uZ29vZ2xlLnByb3RvYnVmLk1lc3NhZ2VPcHRpb25zGLSH",
            "AyABKAg6MwoKaXNfaW5kZXhlZBIdLmdvb2dsZS5wcm90b2J1Zi5GaWVsZE9w",
            "dGlvbnMY8dEeIAEoCEIHqgIEQUVsZmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.Reflection.DescriptorReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pb::Extension[] { OptionsExtensions.Identity, OptionsExtensions.Base, OptionsExtensions.CsharpState, OptionsExtensions.IsView, OptionsExtensions.IsEvent, OptionsExtensions.IsIndexed }, null));
    }
    #endregion

  }
  /// <summary>Holder for extension identifiers generated from the top level of aelf/options.proto</summary>
  internal static partial class OptionsExtensions {
    public static readonly pb::Extension<global::Google.Protobuf.Reflection.FileOptions, string> Identity =
      new pb::Extension<global::Google.Protobuf.Reflection.FileOptions, string>(500001, pb::FieldCodec.ForString(4000010, ""));
    public static readonly pb::RepeatedExtension<global::Google.Protobuf.Reflection.ServiceOptions, string> Base =
      new pb::RepeatedExtension<global::Google.Protobuf.Reflection.ServiceOptions, string>(505001, pb::FieldCodec.ForString(4040010));
    public static readonly pb::Extension<global::Google.Protobuf.Reflection.ServiceOptions, string> CsharpState =
      new pb::Extension<global::Google.Protobuf.Reflection.ServiceOptions, string>(505030, pb::FieldCodec.ForString(4040242, ""));
    public static readonly pb::Extension<global::Google.Protobuf.Reflection.MethodOptions, bool> IsView =
      new pb::Extension<global::Google.Protobuf.Reflection.MethodOptions, bool>(506001, pb::FieldCodec.ForBool(4048008, false));
    public static readonly pb::Extension<global::Google.Protobuf.Reflection.MessageOptions, bool> IsEvent =
      new pb::Extension<global::Google.Protobuf.Reflection.MessageOptions, bool>(50100, pb::FieldCodec.ForBool(400800, false));
    public static readonly pb::Extension<global::Google.Protobuf.Reflection.FieldOptions, bool> IsIndexed =
      new pb::Extension<global::Google.Protobuf.Reflection.FieldOptions, bool>(502001, pb::FieldCodec.ForBool(4016008, false));
  }

}

#endregion Designer generated code
