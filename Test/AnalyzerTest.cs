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
}
