using sena.AST;
using sena.Wasm.Nodes;

namespace sena.Wasm;

public class AST2Wasm
{
    private readonly Root root;

    public AST2Wasm(Root root)
    {
        this.root = root;
    }

    public string Compile()
    {
        return CompileNode(root).ToCode();
    }

    private IWasmNode CompileNode(INode node)
    {
        switch (node)
        {
            case Root root:
                return CompileRoot(root);
            default:
                throw new Exception($"{node.GetType().Name}をIWasmNodeに変換できません。");
        }
    }

    private IWasmNode CompileRoot(Root root)
    {
        var nodes = new List<IWasmNode>();
        nodes.Add(new MemoryNode(1));
        nodes.AddRange(root.Statements.Select(s => CompileNode(s)));
        return new ModuleNode(nodes.ToArray());
    }
}