using AsmResolver.DotNet;
using AsmResolver.DotNet.Serialized;

namespace Il2CppInterop.Generator.MetadataAccess;

internal sealed class Il2CppAssemblyResolver : AssemblyResolverBase
{
    private readonly Dictionary<string, AssemblyDefinition> myCache = new();

    public Il2CppAssemblyResolver() : base(new ModuleReaderParameters())
    {
    }

    public override string? ProbeAssemblyFilePath(AssemblyDescriptor assembly, ModuleDefinition? originModule) => null;

    public void AddToCache(AssemblyDefinition assembly)
    {
        if (!string.IsNullOrEmpty(assembly.Name))
            myCache[assembly.Name!] = assembly;
    }

    public new ResolutionStatus Resolve(AssemblyDescriptor assembly, ModuleDefinition? originModule, out AssemblyDefinition? result)
    {
        if (!string.IsNullOrEmpty(assembly.Name) && myCache.TryGetValue(assembly.Name!, out var cached))
        {
            result = cached;
            return ResolutionStatus.Success;
        }

        return base.Resolve(assembly, originModule, out result);
    }
}
