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

    // 演算子
    PLUS,           // +
    MINUS,          // -
    ASTERISK,       // *
    SLASH,          // /
    PERCENT,        // %
    OR,             // |
    AND,            // &
    BANG,           // !
    EQ,             // =
    NOT_EQ,         // !=
    COND_AND,       // &&
    COND_OR,        // ||
    LT,             // <
    GT,             // >
    LT_EQ,          // <=
    GT_EQ,          // >=

    // グループ
    LPAREN,         // (
    RPAREN,         // )
    LBRACE,         // {
    RBRACE,         // }

    // デリミタ
    CONMA,          // ,
    PERIOD,         // .
    SEMICOLON,      // ;

    // keywords
    INT,            // int
    LET,            // let
    IF,             // if
    ELSE,           // else
    WHILE,          // while
    RETURN,         // return
}
