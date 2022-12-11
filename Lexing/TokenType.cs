namespace sena.Lexing;

public enum TokenType
{
    // 不正
    ILLEGAL,
    // 終端
    EOF,

    IDENTIFIER,         // 識別子
    INTEGER_LITERAL,    // 整数

    LPAREN,             // (
    RPAREN,             // )
    SEMICOLON,          // ;
    ASSIGN,             // =
    MINUS,              // -
    PLUS,               // +
    ASTERISK,           // *
    SLASH,              // /

    TRUE,               // true
    FALSE,              // false

    // keywords
    LET_KEYWORD,
}
