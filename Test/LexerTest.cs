using Xunit.Abstractions;

namespace sena.Test;

public class LexerTest
{
    private readonly ITestOutputHelper Console;

    public LexerTest(ITestOutputHelper testOutputHelper)
    {
        Console = testOutputHelper;
    }

    [Fact]
    public void SemicolonTest()
    {
        var code = @"; ;;; ;;;;;; ;";
        var lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new()
        {
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";"),
            (TokenType.SEMICOLON, ";")
        };

        foreach (var token in tokens)
        {
            var l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.TokenType);
            Assert.Equal(token.literal, l_token.Literal);
        }
    }

    [Fact]
    public void AssignTest()
    {
        var code = @"= =";
        var lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new()
        {
            (TokenType.ASSIGN, "="),
            (TokenType.ASSIGN, "=")
        };

        foreach (var token in tokens)
        {
            var l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.TokenType);
            Assert.Equal(token.literal, l_token.Literal);
        }
    }

    [Fact]
    public void LetKeywordTest()
    {
        var code = @"let";
        var lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new()
        {
            (TokenType.LET_KEYWORD, "let")
        };

        foreach (var token in tokens)
        {
            var l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.TokenType);
            Assert.Equal(token.literal, l_token.Literal);
        }
    }

    [Fact]
    public void MinusPlusTest()
    {
        var code = @"+ - + + - -";
        var lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new()
        {
            (TokenType.PLUS, "+"),
            (TokenType.MINUS, "-"),
            (TokenType.PLUS, "+"),
            (TokenType.PLUS, "+"),
            (TokenType.MINUS, "-"),
            (TokenType.MINUS, "-")
        };

        foreach (var token in tokens)
        {
            var l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.TokenType);
            Assert.Equal(token.literal, l_token.Literal);
        }
    }

    [Fact]
    public void AsteriskSlashTest()
    {
        var code = @"* / * /";
        var lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new()
        {
            (TokenType.ASTERISK, "*"),
            (TokenType.SLASH, "/"),
            (TokenType.ASTERISK, "*"),
            (TokenType.SLASH, "/")
        };

        foreach (var token in tokens)
        {
            var l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.TokenType);
            Assert.Equal(token.literal, l_token.Literal);
        }
    }

    [Fact]
    public void TrueFalseTest()
    {
        var code = @"true false true true false";
        var lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new()
        {
            (TokenType.TRUE, "true"),
            (TokenType.FALSE, "false"),
            (TokenType.TRUE, "true"),
            (TokenType.TRUE, "true"),
            (TokenType.FALSE, "false")
        };

        foreach (var token in tokens)
        {
            var l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.TokenType);
            Assert.Equal(token.literal, l_token.Literal);
        }
    }

    [Fact]
    public void ReturnTest()
    {
        var code = @"return";
        var lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new()
        {
            (TokenType.RETURN_KEYWORD, "return")
        };

        foreach (var token in tokens)
        {
            var l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.TokenType);
            Assert.Equal(token.literal, l_token.Literal);
        }
    }
}