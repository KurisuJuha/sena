namespace sena.Lexing;

public class Lexer
{
    private readonly string _code;
    private int _position;

    public Lexer(string code)
    {
        _code = code;
    }

    private char CurrentChar
    {
        get
        {
            if (_position < _code.Length) return _code[_position];
            return (char)0;
        }
    }

    private char NextChar
    {
        get
        {
            if (_position + 1 < _code.Length) return _code[_position + 1];
            return (char)0;
        }
    }

    public Token NextToken()
    {
        SkipWhiteSpace();
        var retToken = new Token(CurrentChar.ToString(), TokenType.Illegal);

        switch (CurrentChar)
        {
            case ';':
                retToken = new Token(";", TokenType.Semicolon);
                break;
            case '=':
                retToken = new Token("=", TokenType.Assign);
                break;
            case '+':
                retToken = new Token("+", TokenType.Plus);
                break;
            case '-':
                retToken = new Token("-", TokenType.Minus);
                break;
            case '*':
                retToken = new Token("*", TokenType.Asterisk);
                break;
            case '/':
                retToken = new Token("/", TokenType.Slash);
                break;
            case '(':
                retToken = new Token("(", TokenType.Lparen);
                break;
            case ')':
                retToken = new Token(")", TokenType.Rparen);
                break;
            default:
                if (IsDigit(CurrentChar))
                {
                    retToken = new Token(ReadIntegerLiteral(), TokenType.IntegerLiteral);
                }
                else if (IsLetter(CurrentChar))
                {
                    var literal = ReadIdentifier();
                    retToken = new Token(literal, LookUpIdentifier(literal));
                }
                else if (CurrentChar == (char)0)
                {
                    retToken = new Token(CurrentChar.ToString(), TokenType.Eof);
                }

                break;
        }

        ReadChar();
        return retToken;
    }

    private void SkipWhiteSpace()
    {
        while (CurrentChar == ' '
               || CurrentChar == '\t'
               || CurrentChar == '\r'
               || CurrentChar == '\n')
            ReadChar();
    }

    private string ReadIntegerLiteral()
    {
        var literal = CurrentChar.ToString();
        while (IsDigit(NextChar))
        {
            literal += NextChar;

            ReadChar();
        }

        return literal;
    }

    private string ReadIdentifier()
    {
        var literal = CurrentChar.ToString();

        while (IsLetter(NextChar))
        {
            literal += NextChar;
            ReadChar();
        }

        return literal;
    }

    private TokenType LookUpIdentifier(string literal)
    {
        if (Token.Keywords.TryGetValue(literal, out var value)) return value;

        return TokenType.Identifier;
    }

    private void ReadChar()
    {
        _position++;
    }

    private static bool IsLetter(char c)
    {
        return ('a' <= c && c <= 'z')
               || ('A' <= c && c <= 'Z')
               || c == '_';
    }

    private static bool IsDigit(char c)
    {
        return '0' <= c && c <= '9';
    }
}