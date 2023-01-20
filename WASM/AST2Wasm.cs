using sena.AST;
using sena.Wasm.Nodes;

namespace sena.Wasm;

public class AST2Wasm
{
    readonly Root root;

    public AST2Wasm(Root root)
    {
        this.root = root;
    }

    public string Compile() => CompileNode(root).ToCode();

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
        return new ModuleNode(root.statements.Select(s => CompileNode(s)).ToArray());
    }
}
