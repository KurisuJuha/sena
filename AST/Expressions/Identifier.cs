using sena.Lexing;

namespace sena.AST.Expressions;

public class Identifier : IExpression
{
    public readonly string name;

    public Identifier(string name)
    {
        this.name = name;
    }

    public string ToCode()
    {
        return name;
    }
}
