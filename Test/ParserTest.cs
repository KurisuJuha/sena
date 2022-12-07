using sena.AST;
using sena.AST.Expressions;
using sena.AST.Statements;
using sena.Parsing;

namespace sena.Test;

public class ParserTest
{
    [Fact]
    public void LetStatement1()
    {
        var code = @"
let a = 3;
let b = 12314314;
let cfawfaw = 444444444;
";
        Lexer lexer = new Lexer(code);
        Parser parser = new Parser(lexer);
        Root root = parser.Parse();

        Assert.Equal(3, root.statements.Count);

        var names = new List<string>()
        {
            "a",
            "b",
            "cfawfaw"
        };

        for (int i = 0; i < 3; i++)
        {
            LetStatement statement = root.statements[i] as LetStatement;
            string name = statement.name.value;
        }
    }

    [Fact]
    public void LetStatement2()
    {
        var code = @"
hoge;
foo;
piyo;
";
        Lexer lexer = new Lexer(code);
        Parser parser = new Parser(lexer);
        Root root = parser.Parse();

        Assert.Equal(3, root.statements.Count);

        ExpressionStatement expressionStatement1 = root.statements[0] as ExpressionStatement;
        ExpressionStatement expressionStatement2 = root.statements[1] as ExpressionStatement;
        ExpressionStatement expressionStatement3 = root.statements[2] as ExpressionStatement;

        Identifier identifier1 = expressionStatement1.expression as Identifier;
        Identifier identifier2 = expressionStatement2.expression as Identifier;
        Identifier identifier3 = expressionStatement3.expression as Identifier;

        Assert.Equal("hoge", identifier1.value);
        Assert.Equal("foo", identifier2.value);
        Assert.Equal("piyo", identifier3.value);
    }
}
