using AsmResolver.DotNet.Signatures;
using AsmResolver.PE.DotNet.Metadata.Tables;

namespace Il2CppInterop.Generator.Utils;

internal static class MethodSignatureCreator
{
    public static MethodSignature CreateMethodSignature(MethodAttributes attributes, TypeSignature returnType, int genericParameterCount, params TypeSignature[] parameterTypes)
    {
        return CreateMethodSignature((attributes & MethodAttributes.Static) != 0, returnType, genericParameterCount, parameterTypes);
    }

    public static MethodSignature CreateMethodSignature(bool isStatic, TypeSignature returnType, int genericParameterCount, params TypeSignature[] parameterTypes)
    {
        return isStatic ? AsmResolverExtensions.SigCreateStatic(returnType, genericParameterCount, parameterTypes) : AsmResolverExtensions.SigCreateInstance(returnType, genericParameterCount, parameterTypes);
    }
}
