﻿using sena.AST;
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
        builder.AppendLine("(module");
        builder.AppendLine("(memory 1)");
        builder.AppendLine("(export \"memory\" (memory 0))");
        builder.AppendLine(")");

        return builder.ToString();
    }
}
