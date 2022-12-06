namespace sena.Lexing;

public class Lexer
{
    private readonly string code;
    private char currentChar
    {
        get
        {
            if (position < code.Length) return code[position];
            return (char)0;
        }
    }
    private char nextChar
    {
        get
        {
            if (position + 1 < code.Length) return code[position + 1];
            return (char)0;
        }
    }
    private int position;

    public Lexer(string code)
    {
        this.code = code;
    }

    public Token NextToken()
    {
        SkipWhiteSpace();
        Token retToken = new Token();

        switch (currentChar)
        {
            default:
                if (IsDigit(currentChar))
                {
                    retToken = new Token(ReadIntegerLiteral(), TokenType.INTEGER);
                }
                break;
        }

        ReadChar();
        return retToken;
    }

    private void SkipWhiteSpace()
    {
        while (currentChar == ' '
            || currentChar == '\t'
            || currentChar == '\r'
            || currentChar == '\n')
        {
            ReadChar();
        }
    }

    private string ReadIntegerLiteral()
    {
        string literal = currentChar.ToString();
        while (IsDigit(nextChar))
        {
            literal += nextChar;

            ReadChar();
        }

        return literal;
    }

    private void ReadChar()
    {
        position++;
    }

    public static bool IsLetter(char c)
    {
        return ('a' <= c && c <= 'z')
            || ('A' <= c && c <= 'Z')
            || (c == '_');
    }

    public static bool IsDigit(char c)
    {
        return ('0' <= c && c <= '9');
    }
}
