namespace sena.Lexing;

public struct Token
{
    public readonly string Literal;
    public readonly TokenType TokenType;

    public static readonly Dictionary<string, TokenType> Keywords = new()
    {
        ["let"] = TokenType.LetKeyword,
        ["true"] = TokenType.True,
        ["false"] = TokenType.False,
        ["return"] = TokenType.ReturnKeyword
    };

    public Token()
    {
        Literal = "";
        TokenType = TokenType.Illegal;
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