using sena.AST;
using sena.AST.Expressions;
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

    private bool AnalyzeExpression(IExpression expression)
    {
        switch (expression)
        {
            case IntLiteral intLiteral:
                return AnalyzeIntLiteralExpression(intLiteral);
            default:
                return false;
        }
    }

    #region Statements
    private bool AnalyzeLetStatement(LetStatement letStatement)
    {
        if (!AnalyzeExpression(letStatement.value)) return false;

        return true;
    }
    #endregion

    #region Expressions
    private bool AnalyzeIntLiteralExpression(IntLiteral intLiteral)
    {
        return true;
    }
    #endregion
}
