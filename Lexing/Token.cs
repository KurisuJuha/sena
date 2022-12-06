namespace sena.Lexing;

public struct Token
{
    public readonly string literal;
    public readonly TokenType tokenType;
    public static readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>
    {

    };

    public Token() => new Token("", TokenType.ILLEGAL);

    public Token(string literal, TokenType tokenType)
    {
        this.literal = literal;
        this.tokenType = tokenType;
    }
}
