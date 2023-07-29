namespace sena.AST.Expressions;

public class InfixExpression : IExpression
{
    private readonly string _op;
    public readonly IExpression LeftExpression;
    public readonly IExpression RightExpression;

    public InfixExpression(string op, IExpression rightExpression, IExpression leftExpression)
    {
        _op = op;
        RightExpression = rightExpression;
        LeftExpression = leftExpression;
    }

    public string ToCode()
    {
        return $"( {LeftExpression.ToCode()} {_op} {RightExpression.ToCode()} )";
    }
}