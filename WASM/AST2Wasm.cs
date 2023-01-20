using sena.AST;
using System.Text;

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
            case Root root:
                return CompileRoot(root);
            default:
                return "";
        }
    }

    private string CompileRoot(Root root)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("(module");
        builder.Append("(memory 1)");
        builder.Append("(export \"memory\" (memory 0))");
        builder.Append(")");

        return builder.ToString();
    }
}
