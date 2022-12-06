namespace sena.Lexing;

public struct Token
{
    public readonly string literal;
    public readonly TokenType tokenType;

    public Token() => new Token("", TokenType.ILLEGAL);

    public Token(string literal, TokenType tokenType)
    {
        this.literal = literal;
        this.tokenType = tokenType;
    }
}
