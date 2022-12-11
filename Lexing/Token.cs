namespace sena.Lexing;

public struct Token
{
    public readonly string literal;
    public readonly TokenType tokenType;
    public static readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>
    {
        ["let"] = TokenType.LET_KEYWORD,
        ["true"] = TokenType.TRUE,
        ["false"] = TokenType.FALSE,
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

    public override string ToString()
    {
        return $"{{literal : {literal} tokenType : {tokenType}}}";
    }
}
