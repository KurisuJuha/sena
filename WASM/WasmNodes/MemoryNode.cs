namespace sena.Wasm.Nodes;

public class MemoryNode : IWasmNode
{
    public readonly int size;

    public MemoryNode(int size)
    {
        this.size = size;
    }

    public string ToCode()
    {
        return $"(memory {size})";
    }
}