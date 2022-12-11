using sena.AST;
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
        string code = @"let piyo = poyo; let hoge = -123 + 4 + -100 + a;";

        Lexer lexer = new Lexer(code);
        Errors errors = new Errors();
        Parser parser = new Parser(lexer, errors, Console.WriteLine);
        Root root = parser.Parse();

        errors.WriteLine(Console.WriteLine);

        Assert.Equal(0, errors.errors.Count);

        Assert.Equal(2, root.statements.Count);
        LetStatement? letStatement = root.statements[0] as LetStatement;
        Assert.NotNull(letStatement);
        LetStatement? letStatement2 = root.statements[1] as LetStatement;
        Assert.NotNull(letStatement2);
        Console.WriteLine(root.ToCode());
    }

    [Fact]
    public void PrefixInfixTest()
    {
        string code = @"let a = (100 + 100 )* -10 / 2;";

        Lexer lexer = new Lexer(code);
        Errors errors = new Errors();
        Parser parser = new Parser(lexer, errors, Console.WriteLine);
        Root root = parser.Parse();

        errors.WriteLine(Console.WriteLine);

        Assert.Equal(0, errors.errors.Count);

        Console.WriteLine(root.ToCode());
    }

    [Fact]
    public void ExpressionStatementTest()
    {
        string code = @"hoge; foo;";

        Lexer lexer = new Lexer(code);
        Errors errors = new Errors();
        Parser parser = new Parser(lexer, errors, Console.WriteLine);
        Root root = parser.Parse();

        errors.WriteLine(Console.WriteLine);

        Assert.Equal(0, errors.errors.Count);

        Console.WriteLine(root.ToCode());
    }
}
