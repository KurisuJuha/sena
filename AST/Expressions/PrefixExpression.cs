namespace sena.AST.Expressions;

public class PrefixExpression : IExpression
{
    public string op;
    public IExpression rightExpression;

    public PrefixExpression(string op, IExpression rightExpression)
    {
        this.op = op;
        this.rightExpression = rightExpression;
    }

    public string ToCode()
    {
        return "(" + op + rightExpression.ToCode() + ")";
    }
}
