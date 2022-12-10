namespace sena.Lexing;

public enum TokenType
{
    // 不正
    ILLEGAL,
    // 終端
    EOF,

    // 識別子
    IDENTIFIER,
    // 整数
    INTEGER_LITERAL,
    // ;
    SEMICOLON,
    ASSIGN,         // =
    MINUS,          // -
    PLUS,           // +
    ASTERISK,       // *
    SLASH,          // /

    // keywords
    LET_KEYWORD,
}
