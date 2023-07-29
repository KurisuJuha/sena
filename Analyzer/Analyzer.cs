﻿using sena.AST;
using sena.AST.Expressions;
using sena.AST.Statements;

namespace sena.Analyzing;

public class Analyzer
{
    private readonly Root root;
    public Dictionary<string, ExpressionData> variableNames;

    public Analyzer(Root root)
    {
        this.root = root;
        variableNames = new Dictionary<string, ExpressionData>();
    }

    public bool Analyze()
    {
        return root.Statements.All(AnalyzeStatement);
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
        if (AnalyzeExpression(expressionStatement.Expression) is null) return false;

        return true;
    }

    private bool AnalyzeLetStatement(LetStatement letStatement)
    {
        // 変数の中身について
        var expressionData = AnalyzeExpression(letStatement.Value);
        if (expressionData is null) return false;

        // 変数の名前について
        if (variableNames.Keys.Contains(letStatement.Identifier.Name)) return false;

        variableNames[letStatement.Identifier.Name] = expressionData;
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
        if (!variableNames.Keys.Contains(identifier.Name)) return null;

        return new ExpressionData(variableNames[identifier.Name].typeName);
    }

    private ExpressionData? AnalyzeBoolLiteralExpression(BoolLiteral boolLiteral)
    {
        return new ExpressionData("sena.Boolean");
    }

    private ExpressionData? AnalyzeInfixExpression(InfixExpression infixExpression)
    {
        var left = AnalyzeExpression(infixExpression.leftExpression);
        var right = AnalyzeExpression(infixExpression.rightExpression);

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
        var expressionData = AnalyzeExpression(prefixExpression.expression);

        if (expressionData == null) return null;

        return expressionData;
    }

    #endregion
}