using sena.AST;
using sena.AST.Statements;
using System.Collections.ObjectModel;

namespace sena.Analysis;

public class Analyzer
{
    readonly Root root;

    public Analyzer(Root root)
    {
        this.root = root;
    }

    public bool Analyze()
    {
        return root.statements.All(AnalyzeStatement);
    }

    private bool AnalyzeStatement(IStatement statement)
    {
        switch (statement)
        {
            case LetStatement letStatement:
                return AnalyzeLetStatement(letStatement);
            default:
                return false;
        }
    }

    private bool AnalyzeLetStatement(LetStatement letStatement)
    {
        return true;
    }
}
