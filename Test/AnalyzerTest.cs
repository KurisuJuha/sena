using sena.Parsing;
using sena.SemanticsAnalysis;
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
        SemanticsAnalyzer semanticsAnalyzer = new SemanticsAnalyzer(parser.Parse());

        Assert.True(semanticsAnalyzer.Analyze());
    }
}
