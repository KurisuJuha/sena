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
}
