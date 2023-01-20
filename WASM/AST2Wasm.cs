using sena.AST;

namespace sena.Wasm;

public class AST2Wasm
{
    readonly Root root;

    public AST2Wasm(Root root)
    {
        this.root = root;
    }

    public string Compile() => CompileNode(root);

    private string CompileNode(INode node)
    {
        switch (node)
        {
            default:
                return "";
        }
    }
}
