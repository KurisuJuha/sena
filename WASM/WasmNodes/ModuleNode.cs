using System.Collections.ObjectModel;

namespace sena.Wasm.Nodes;

public class ModuleNode : IWasmNode
{
    public readonly ReadOnlyCollection<IWasmNode> nodes;

    public ModuleNode(IWasmNode[] nodes)
    {
        this.nodes = nodes.AsReadOnly();
    }

    public string ToCode()
    {
        return $"(module{string.Join("", nodes.Select(n => n.ToCode()))})";
    }
}