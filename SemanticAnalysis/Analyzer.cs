using sena.Analyzing;
using sena.AST;
using sena.AST.Expressions;
using sena.AST.Statements;
using System.Collections.ObjectModel;

namespace sena.Analysis;

public class Analyzer
{
    readonly Root root;
    public Dictionary<string, ExpressionData> variableNames;

    public Analyzer(Root root)
    {
        this.root = root;
        variableNames = new Dictionary<string, ExpressionData>();
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

    private ExpressionData? AnalyzeExpression(IExpression expression)
    {
        switch (expression)
        {
            case IntLiteral intLiteral:
                return AnalyzeIntLiteralExpression(intLiteral);
            case BoolLiteral boolLiteral:
                return AnalyzeBoolLiteralExpression(boolLiteral);
            case Identifier identifier:
                return AnalyzeIdentifierExpression(identifier);
            case InfixExpression infixExpression:
                return AnalyzeInfixExpression(infixExpression);
            case PrefixExpression prefixExpression:
                return AnalyzePrefixExpression(prefixExpression);
            default:
                return null;
        }
    }

    #region Statements
    private bool AnalyzeExpressionStatement(ExpressionStatement expressionStatement)
    {
        // 中身の解析
        if (AnalyzeExpression(expressionStatement.expression) is null) return false;

        return true;
    }

    private bool AnalyzeLetStatement(LetStatement letStatement)
    {
        // 変数の中身について
        ExpressionData? expressionData = AnalyzeExpression(letStatement.value);
        if (expressionData is null) return false;

        // 変数の名前について
        if (variableNames.Keys.Contains(letStatement.identifier.name)) return false;

        variableNames[letStatement.identifier.name] = expressionData;
        return true;
    }
    #endregion

    #region Expressions
    private ExpressionData? AnalyzeIntLiteralExpression(IntLiteral intLiteral)
    {
        return new ExpressionData("sena.Integer");
    }

    private ExpressionData? AnalyzeIdentifierExpression(Identifier identifier)
    {
        // このidentifierの名前が存在するかどうか
        if (!variableNames.Keys.Contains(identifier.name)) return null;

        return new ExpressionData(variableNames[identifier.name].typeName);
    }

    private ExpressionData? AnalyzeBoolLiteralExpression(BoolLiteral boolLiteral)
    {
        return new ExpressionData("sena.Boolean");
    }

    private ExpressionData? AnalyzeInfixExpression(InfixExpression infixExpression)
    {
        ExpressionData? left = AnalyzeExpression(infixExpression.leftExpression);
        ExpressionData? right = AnalyzeExpression(infixExpression.rightExpression);

        // 左がnullか
        if (left == null) return null;

        // 右がnullか
        if (right == null) return null;

        // 右と左が同じ型か
        if (right.typeName != left.typeName) return null;

        return new ExpressionData(left.typeName);
    }

    private ExpressionData? AnalyzePrefixExpression(PrefixExpression prefixExpression)
    {
        ExpressionData? expressionData = AnalyzeExpression(prefixExpression.expression);

        if (expressionData == null) return null;

        return expressionData;
    }
    #endregion
}
