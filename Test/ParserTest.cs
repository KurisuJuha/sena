using sena.AST.Expressions;
using sena.AST.Statements;
using sena.Parsing;
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
    public void LetStatementTest()
    {
        var code = @"let piyo = poyo; let hoge = -123 + 4 + -100 + a;";

        var lexer = new Lexer(code);
        var errors = new Errors();
        var parser = new Parser(lexer, errors, Console.WriteLine);
        var root = parser.Parse();

        errors.WriteLine(Console.WriteLine);

        Assert.Equal(0, errors.ErrorList.Count);

        Assert.Equal(2, root.Statements.Count);
        var letStatement = root.Statements[0] as LetStatement;
        Assert.NotNull(letStatement);
        var letStatement2 = root.Statements[1] as LetStatement;
        Assert.NotNull(letStatement2);
        Console.WriteLine(root.ToCode());
    }

    [Fact]
    public void ReturnStatementTest()
    {
        var code = @"return 123;";

        var lexer = new Lexer(code);
        var errors = new Errors();
        var parser = new Parser(lexer, errors, Console.WriteLine);
        var root = parser.Parse();

        errors.WriteLine(Console.WriteLine);

        Assert.Equal(0, errors.ErrorList.Count);

        Assert.Equal("return 123;", root.ToCode());

        Console.WriteLine(root.ToCode());
    }

    [Fact]
    public void PrefixInfixTest()
    {
        var code = @"let a = (100 + 100 )* -10 / 2;";

        var lexer = new Lexer(code);
        var errors = new Errors();
        var parser = new Parser(lexer, errors, Console.WriteLine);
        var root = parser.Parse();

        errors.WriteLine(Console.WriteLine);

        Assert.Equal(0, errors.ErrorList.Count);

        Console.WriteLine(root.ToCode());
    }

    [Fact]
    public void ExpressionStatementTest()
    {
        var code = @"hoge; foo;";

        var lexer = new Lexer(code);
        var errors = new Errors();
        var parser = new Parser(lexer, errors, Console.WriteLine);
        var root = parser.Parse();

        errors.WriteLine(Console.WriteLine);

        Assert.Equal(0, errors.ErrorList.Count);

        Console.WriteLine(root.ToCode());
    }

    [Fact]
    public void BoolLiteralTest()
    {
        var code = @"
true;
false;
";
        var lexer = new Lexer(code);
        var errors = new Errors();
        var parser = new Parser(lexer, errors);
        var root = parser.Parse();

        Console.WriteLine(root.ToCode());
        var boolLiteral1 = (root.Statements[0] as ExpressionStatement)?.Expression as BoolLiteral;
        var boolLiteral2 = (root.Statements[1] as ExpressionStatement)?.Expression as BoolLiteral;

        Assert.NotNull(boolLiteral1);
        Assert.NotNull(boolLiteral2);
    }
}