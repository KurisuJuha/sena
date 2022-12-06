namespace sena.Test;

public class LexerTest
{
    [Fact]
    public void NextToken1()
    {
        Lexer lexer = new Lexer("^ aiueo 123");

        var tokenList = new List<Token>()
        {
            new Token("^", TokenType.ILLEGAL),
            new Token("aiueo", TokenType.IDENTIFIER),
            new Token("123", TokenType.INTEGER),
        };

        foreach (var testToken in tokenList)
        {
            var token = lexer.NextToken();

            Assert.Equal(testToken.literal, token.literal);
            Assert.Equal(testToken.tokenType, token.tokenType);
        }
    }

    [Fact]
    public void NextToken2()
    {
        Lexer lexer = new Lexer("123 1234 98493829");

        var tokenList = new List<Token>()
        {
            new Token("123", TokenType.INTEGER),
            new Token("1234", TokenType.INTEGER),
            new Token("98493829", TokenType.INTEGER),
        };

        foreach (var testToken in tokenList)
        {
            var token = lexer.NextToken();

            Assert.Equal(testToken.literal, token.literal);
            Assert.Equal(testToken.tokenType, token.tokenType);
        }
    }

    [Fact]
    public void NextToken3()
    {
        Lexer lexer = new Lexer("+ - * / % | & ! =");

        var tokenList = new List<Token>()
        {
            new Token("+", TokenType.PLUS),
            new Token("-", TokenType.MINUS),
            new Token("*", TokenType.ASTERISK),
            new Token("/", TokenType.SLASH),
            new Token("%", TokenType.PERCENT),
            new Token("|", TokenType.OR),
            new Token("&", TokenType.AND),
            new Token("!", TokenType.BANG),
            new Token("=", TokenType.EQ),
        };

        foreach (var testToken in tokenList)
        {
            var token = lexer.NextToken();

            Assert.Equal(testToken.literal, token.literal);
            Assert.Equal(testToken.tokenType, token.tokenType);
        }
    }

    [Fact]
    public void NextToken4()
    {
        Lexer lexer = new Lexer("!= && || < > , . ;");

        var tokenList = new List<Token>()
        {
            new Token("!=", TokenType.NOT_EQ),
            new Token("&&", TokenType.COND_AND),
            new Token("||", TokenType.COND_OR),
            new Token("<", TokenType.LT),
            new Token(">", TokenType.GT),
            new Token(",", TokenType.CONMA),
            new Token(".", TokenType.PERIOD),
            new Token(";", TokenType.SEMICOLON)
        };

        foreach (var testToken in tokenList)
        {
            var token = lexer.NextToken();

            Assert.Equal(testToken.literal, token.literal);
            Assert.Equal(testToken.tokenType, token.tokenType);
        }
    }

    [Fact]
    public void IsLetter1()
    {
        var letterList = new List<char>()
        {
            'a',
            'b',
            'c',
            'd',
            'l',
            'm',
            'n',
            'x',
            'y',
            'z',
        };

        foreach (var c in letterList)
        {
            Assert.True(Lexer.IsLetter(c));
        }
    }

    [Fact]
    public void IsLetter2()
    {
        var letterList = new List<char>()
        {
            ':',
            '[',
            ']',
            '~',
            '=',
            '#',
        };

        foreach (var c in letterList)
        {
            Assert.False(Lexer.IsLetter(c));
        }
    }

    [Fact]
    public void IsDigit1()
    {
        var digitList = new List<char>()
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
        };

        foreach (var d in digitList)
        {
            Assert.True(Lexer.IsDigit(d));
        }
    }

    [Fact]
    public void IsDigit2()
    {
        var digitList = new List<char>()
        {
            ':',
            '[',
            ']',
            '~',
            '=',
            '#',
        };

        foreach (var d in digitList)
        {
            Assert.False(Lexer.IsLetter(d));
        }
    }
}
