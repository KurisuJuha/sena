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
        switch (currentChar)
        {
            default:
                return new Token();
        }
    }

    private void ReadChar()
    {
        position++;
    }

    private bool IsLetter(char c)
    {
        return ('a' <= c && c <= 'z')
            || ('A' <= c && c <= 'Z')
            || (c == '_');
    }
}
