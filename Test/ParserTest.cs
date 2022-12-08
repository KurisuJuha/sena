using sena.AST;
using sena.AST.Expressions;
using sena.AST.Statements;
using sena.Parsing;
using System.Linq;
using Xunit.Abstractions;

namespace sena.Test;

public class ParserTest
{
    private readonly ITestOutputHelper Console;
    public ParserTest(ITestOutputHelper testOutputHelper)
    {
        Console = testOutputHelper;
    }

    [Fact]
    public void LetStatement1()
    {
        var code = @"
let a = 3;
let b = 12314314;
let cfawfaw = 444444444;
";
        Errors errors = new Errors();
        Lexer lexer = new Lexer(code);
        Parser parser = new Parser(lexer, errors);
        Root root = parser.Parse();

        errors.WriteLine(Console.WriteLine);

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
            string name = statement.name.name;
        }
    }

    [Fact]
    public void ExpressionStatement1()
    {
        var code = @"
hoge;
foo;
piyo;
";
        Errors errors = new Errors();
        Lexer lexer = new Lexer(code);
        Parser parser = new Parser(lexer, errors);
        Root root = parser.Parse();

        errors.WriteLine(Console.WriteLine);

        Assert.Equal(3, root.statements.Count);

        ExpressionStatement expressionStatement1 = root.statements[0] as ExpressionStatement;
        ExpressionStatement expressionStatement2 = root.statements[1] as ExpressionStatement;
        ExpressionStatement expressionStatement3 = root.statements[2] as ExpressionStatement;

        Identifier identifier1 = expressionStatement1.expression as Identifier;
        Identifier identifier2 = expressionStatement2.expression as Identifier;
        Identifier identifier3 = expressionStatement3.expression as Identifier;

        Assert.Equal("hoge", identifier1.name);
        Assert.Equal("foo", identifier2.name);
        Assert.Equal("piyo", identifier3.name);
    }

    [Fact]
    public void ReturnStatement1()
    {
        var code = @"
return a;
return b;
return ffffff;
";
        Errors errors = new Errors();
        Lexer lexer = new Lexer(code);
        Parser parser = new Parser(lexer, errors);
        Root root = parser.Parse();

        errors.WriteLine(Console.WriteLine);
        Console.WriteLine(root.ToCode());

        Assert.Equal(3, root.statements.Count);

        List<string> names = new List<string>()
        {
            "a",
            "b",
            "ffffff",
        };

        List<string> parsedNames = root.statements.Take(3).Select(s => ((Identifier)((ReturnStatement)s).expression).name).ToList();

        for (int i = 0; i < names.Count; i++)
        {
            Assert.Equal(names[i], parsedNames[i]);
        }
    }
}
