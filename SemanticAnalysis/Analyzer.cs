using sena.AST;
using sena.AST.Expressions;
using sena.AST.Statements;
using System.Collections.ObjectModel;

namespace sena.Analysis;

public class Analyzer
{
    readonly Root root;
    public List<string> variableNames;

    public Analyzer(Root root)
    {
        this.root = root;
        this.variableNames = new List<string>();
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
            case ExpressionStatement expressionStatement:
                return AnalyzeExpressionStatement(expressionStatement);
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
        // 変数の中身について
        if (!AnalyzeExpression(letStatement.value)) return false;

        // 変数の名前について
        if (variableNames.Contains(letStatement.identifier.name)) return false;

        return true;
    }

    private bool AnalyzeExpressionStatement(ExpressionStatement expressionStatement)
    {
        // 中身の解析
        if (!AnalyzeExpression(expressionStatement.expression)) return false;

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
