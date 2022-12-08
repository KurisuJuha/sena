namespace sena.AST.Expressions;

public class PrefixExpression : IExpression
{
    public string op;
    public IExpression leftExpression;

    public PrefixExpression(string op, IExpression leftExpression)
    {
        this.op = op;
        this.leftExpression = leftExpression;
    }

    public string ToCode()
    {
        return "(" + op + leftExpression.ToCode() + ")";
    }
}
