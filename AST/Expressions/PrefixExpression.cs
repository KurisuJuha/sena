namespace sena.AST.Expressions;

public class PrefixExpression : IExpression
{
    private readonly string _op;
    public readonly IExpression Expression;

    public PrefixExpression(string op, IExpression expression)
    {
        _op = op;
        Expression = expression;
    }

    public string ToCode()
    {
        return $"( {_op}{Expression.ToCode()} )";
    }
}