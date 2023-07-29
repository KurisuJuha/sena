namespace sena.AST.Expressions;

public class BoolLiteral : IExpression
{
    private readonly bool _value;

    public BoolLiteral(bool value)
    {
        _value = value;
    }

    public string ToCode()
    {
        return _value ? "true" : "false";
    }
}