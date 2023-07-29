namespace sena.Lexing;

public enum TokenType
{
    // 不正
    Illegal,

    // 終端
    Eof,

    Identifier, // 識別子
    IntegerLiteral, // 整数

    Lparen, // (
    Rparen, // )
    Semicolon, // ;
    Assign, // =
    Minus, // -
    Plus, // +
    Asterisk, // *
    Slash, // /

    True, // true
    False, // false

    // keywords
    LetKeyword,
    ReturnKeyword
}