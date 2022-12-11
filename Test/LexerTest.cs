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
        string code = @"; ;;; ;;;;;; ;";
        Lexer lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new List<(TokenType, string)>()
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
            (TokenType.SEMICOLON, ";"),
        };

        foreach (var token in tokens)
        {
            Token l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.tokenType);
            Assert.Equal(token.literal, l_token.literal);
        }
    }

    [Fact]
    public void AssignTest()
    {
        string code = @"= =";
        Lexer lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new List<(TokenType, string)>()
        {
            (TokenType.ASSIGN, "="),
            (TokenType.ASSIGN, "="),
        };

        foreach (var token in tokens)
        {
            Token l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.tokenType);
            Assert.Equal(token.literal, l_token.literal);
        }
    }

    [Fact]
    public void LetKeywordTest()
    {
        string code = @"let";
        Lexer lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new List<(TokenType, string)>()
        {
            (TokenType.LET_KEYWORD, "let"),
        };

        foreach (var token in tokens)
        {
            Token l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.tokenType);
            Assert.Equal(token.literal, l_token.literal);
        }
    }

    [Fact]
    public void MinusPlusTest()
    {
        string code = @"+ - + + - -";
        Lexer lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new List<(TokenType, string)>()
        {
            (TokenType.PLUS, "+"),
            (TokenType.MINUS, "-"),
            (TokenType.PLUS, "+"),
            (TokenType.PLUS, "+"),
            (TokenType.MINUS, "-"),
            (TokenType.MINUS, "-"),
        };

        foreach (var token in tokens)
        {
            Token l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.tokenType);
            Assert.Equal(token.literal, l_token.literal);
        }
    }

    [Fact]
    public void AsteriskSlashTest()
    {
        string code = @"* / * /";
        Lexer lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new List<(TokenType, string)>()
        {
            (TokenType.ASTERISK, "*"),
            (TokenType.SLASH, "/"),
            (TokenType.ASTERISK, "*"),
            (TokenType.SLASH, "/"),
        };

        foreach (var token in tokens)
        {
            Token l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.tokenType);
            Assert.Equal(token.literal, l_token.literal);
        }
    }

    [Fact]
    public void TrueFalseTest()
    {
        string code = @"true false true true false";
        Lexer lexer = new Lexer(code);

        List<(TokenType type, string literal)> tokens = new List<(TokenType, string)>()
        {
            (TokenType.TRUE, "true"),
            (TokenType.FALSE, "false"),
            (TokenType.TRUE, "true"),
            (TokenType.TRUE, "true"),
            (TokenType.FALSE, "false"),
        };

        foreach (var token in tokens)
        {
            Token l_token = lexer.NextToken();

            Assert.Equal(token.type, l_token.tokenType);
            Assert.Equal(token.literal, l_token.literal);
        }
    }
}
