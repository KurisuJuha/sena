namespace sena.AST.Expressions;

public class InfixExpression : IExpression
{
    public readonly string op;
    public readonly IExpression leftExpression;
    public readonly IExpression rightExpression;

    public InfixExpression(string op, IExpression leftExpression, IExpression rightExpression)
    {
        this.op = op;
        this.leftExpression = leftExpression;
        this.rightExpression = rightExpression;
    }

    public string ToCode()
    {
        return $"({leftExpression.ToCode()} {op} {rightExpression.ToCode()})";
    }
}
