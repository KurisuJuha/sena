namespace sena.AST.Expressions;

public class Identifier : IExpression
{
    public readonly string Name;

    public Identifier(string name)
    {
        Name = name;
    }

    public string ToCode()
    {
        return Name;
    }
}