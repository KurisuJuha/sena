namespace sena.AST.Expressions;

public class PrefixExpression : IExpression
{
    public readonly string op;
    public readonly IExpression expression;

    public PrefixExpression(string op, IExpression expression)
    {
        this.op = op;
        this.expression = expression;
    }

    public string ToCode()
    {
        return $"( {op}{expression.ToCode()} )";
    }
}
