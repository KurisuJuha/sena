using sena.Analysis;
using sena.Parsing;
using Xunit.Abstractions;

namespace sena.Test;

public class AnalyzerTest
{
    private readonly ITestOutputHelper Console;
    public AnalyzerTest(ITestOutputHelper testOutputHelper)
    {
        Console = testOutputHelper;
    }

    [Fact]
    public void AnalyzeLetStatementTest()
    {
        string code = $"let a = 1;";

        Lexer lexer = new Lexer(code);
        Errors errors = new Errors();
        Parser parser = new Parser(lexer, errors, Console.WriteLine);
        Analyzer semanticsAnalyzer = new Analyzer(parser.Parse());

        Assert.True(semanticsAnalyzer.Analyze());
    }

    [Fact]
    public void AnalyzeExpressionStatementTest()
    {
        string code = $"19;23;444; 505;";

        Lexer lexer = new Lexer(code);
        Errors errors = new Errors();
        Parser parser = new Parser(lexer, errors, Console.WriteLine);
        Analyzer semanticsAnalyzer = new Analyzer(parser.Parse());

        Assert.True(semanticsAnalyzer.Analyze());
    }

    [Fact]
    public void AnalyzeIdentifierExpressionTest()
    {
        string code = $"let b = 10; let fafweafwa = 33; let hoge = 444444; let HogeHoge =44; b; fafweafwa; hoge; HogeHoge;";

        Lexer lexer = new Lexer(code);
        Errors errors = new Errors();
        Parser parser = new Parser(lexer, errors, Console.WriteLine);
        Analyzer semanticsAnalyzer = new Analyzer(parser.Parse());

        Assert.True(semanticsAnalyzer.Analyze());
    }

    [Fact]
    public void AnalyzeInfixExpressionTest()
    {
        string code = $"333443 + 443434;";

        Lexer lexer = new Lexer(code);
        Errors errors = new Errors();
        Parser parser = new Parser(lexer, errors, Console.WriteLine);
        Analyzer semanticsAnalyzer = new Analyzer(parser.Parse());

        Assert.True(semanticsAnalyzer.Analyze());
    }

    [Fact]
    public void AnalyzePrefixExpressionTest()
    {
        string code = $"-12;";

        Lexer lexer = new Lexer(code);
        Errors errors = new Errors();
        Parser parser = new Parser(lexer, errors, Console.WriteLine);
        Analyzer semanticsAnalyzer = new Analyzer(parser.Parse());

        Assert.True(semanticsAnalyzer.Analyze());
    }
}
