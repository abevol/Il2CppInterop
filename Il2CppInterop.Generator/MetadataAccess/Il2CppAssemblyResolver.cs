using System.Collections.Generic;
using AsmResolver.DotNet;

namespace Il2CppInterop.Generator.MetadataAccess;

internal sealed class Il2CppAssemblyResolver : IAssemblyResolver
{
    private readonly Dictionary<string, AssemblyDefinition> _cache = new();

    public void AddToCache(AssemblyDefinition assembly)
    {
        if (assembly.Name is not null)
            _cache[assembly.Name] = assembly;
    }

    public void ClearCache()
    {
        _cache.Clear();
    }

    public ResolutionStatus Resolve(AssemblyDescriptor assembly, ModuleDefinition? originModule, out AssemblyDefinition? result)
    {
        if (assembly.Name is not null && _cache.TryGetValue(assembly.Name, out var cached))
        {
            result = cached;
            return ResolutionStatus.Success;
        }

        result = null;
        return ResolutionStatus.AssemblyNotFound;
    }
}
