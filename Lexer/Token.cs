namespace sena.Lexing;

public struct Token
{
    public readonly string Literal;
    public readonly TokenType TokenType;

    public static readonly Dictionary<string, TokenType> Keywords = new()
    {
        ["let"] = TokenType.LET_KEYWORD,
        ["true"] = TokenType.TRUE,
        ["false"] = TokenType.FALSE,
        ["return"] = TokenType.RETURN_KEYWORD
    };

    public Token()
    {
        Literal = "";
        TokenType = TokenType.ILLEGAL;
    }

    public Token(string literal, TokenType tokenType)
    {
        Literal = literal;
        TokenType = tokenType;
    }

    public override string ToString()
    {
        return $"{{literal : {Literal} tokenType : {TokenType}}}";
    }
}