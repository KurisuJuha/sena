namespace sena.Lexing;

public struct Token
{
    public readonly string literal;
    public readonly TokenType tokenType;
    public static readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>
    {
        ["let"] = TokenType.LET,
        ["if"] = TokenType.IF,
        ["else"] = TokenType.ELSE,
        ["while"] = TokenType.WHILE,
        ["return"] = TokenType.RETURN,
    };

    public Token()
    {
        literal = "";
        tokenType = TokenType.ILLEGAL;
    }

    public Token(string literal, TokenType tokenType)
    {
        this.literal = literal;
        this.tokenType = tokenType;
    }
}
