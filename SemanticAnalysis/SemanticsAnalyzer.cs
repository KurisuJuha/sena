using sena.AST;

namespace sena.SemanticsAnalysis;

public class SemanticsAnalyzer
{
    readonly Root root;

    public SemanticsAnalyzer(Root root)
    {
        this.root = root;
    }

    public bool Analyze()
    {
        return root.statements.All(AnalyzeStatement);
    }

    public bool AnalyzeStatement(IStatement statement)
    {
        switch (statement)
        {
            default:
                return false;
        }
    }
}