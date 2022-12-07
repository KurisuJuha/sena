using sena.Lexing;

namespace sena.AST.Expressions;

public class Identifier : IExpression
{
    public readonly Token token;
    public readonly string value;

    public Identifier(Token token, string value)
    {
        this.token = token;
        this.value = value;
    }
}
