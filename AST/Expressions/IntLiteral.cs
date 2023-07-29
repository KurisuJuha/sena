namespace sena.AST.Expressions;

public class IntLiteral : IExpression
{
    private readonly string _value;

    public IntLiteral(string value)
    {
        _value = value;
    }

    public string ToCode()
    {
        return _value;
    }
}