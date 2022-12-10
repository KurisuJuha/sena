namespace sena.AST.Expressions;

public class InfixExpression : IExpression
{
    public readonly string op;
    public readonly IExpression rightExpression;
    public readonly IExpression leftExpression;

    public InfixExpression(string op, IExpression rightExpression, IExpression leftExpression)
    {
        this.op = op;
        this.rightExpression = rightExpression;
        this.leftExpression = leftExpression;
    }

    public string ToCode()
    {
        return $"( {rightExpression.ToCode()} {op} {leftExpression.ToCode()} )";
    }
}
