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
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";"),
            (TokenType.Semicolon, ";")
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
            (TokenType.Assign, "="),
            (TokenType.Assign, "=")
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
            (TokenType.LetKeyword, "let")
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
            (TokenType.Plus, "+"),
            (TokenType.Minus, "-"),
            (TokenType.Plus, "+"),
            (TokenType.Plus, "+"),
            (TokenType.Minus, "-"),
            (TokenType.Minus, "-")
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
            (TokenType.Asterisk, "*"),
            (TokenType.Slash, "/"),
            (TokenType.Asterisk, "*"),
            (TokenType.Slash, "/")
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
            (TokenType.True, "true"),
            (TokenType.False, "false"),
            (TokenType.True, "true"),
            (TokenType.True, "true"),
            (TokenType.False, "false")
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
            (TokenType.ReturnKeyword, "return")
        };

        foreach (var token in tokens)
        {
            var l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.TokenType);
            Assert.Equal(token.literal, l_token.Literal);
        }
    }
}