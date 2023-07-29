namespace sena.AST.Statements;

public class ExpressionStatement : IStatement
{
    public readonly IExpression Expression;

    public ExpressionStatement(IExpression expression)
    {
        Expression = expression;
    }

    public string ToCode()
    {
        return $"{Expression.ToCode()};";
    }
}