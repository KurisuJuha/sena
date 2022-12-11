namespace sena.AST.Expressions;

public class BoolLiteral : IExpression
{
    public readonly bool value;

    public BoolLiteral(bool value)
    {
        this.value = value;
    }

    public string ToCode()
    {
        return value ? "true" : "false";
    }
}
