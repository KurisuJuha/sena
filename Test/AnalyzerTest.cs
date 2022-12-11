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
        string code = @"let a = 1;";
        Assert.True(GetAnalyzer(code).Analyze());

        // 存在している変数名を使用して新しい変数を作成しようとした場合
        string code2 = @"
let b = 1;
let b = 2;";
        Assert.False(GetAnalyzer(code2).Analyze());
    }

    [Fact]
    public void AnalyzeExpressionStatementTest()
    {
        string code = @"
19;
23;
444; 
505;
true;
false;";
        Assert.True(GetAnalyzer(code).Analyze());
    }

    [Fact]
    public void AnalyzeIdentifierExpressionTest()
    {
        string code = @"
let b = 10;
let fafweafwa = 33; 
let hoge = 444444; 
let HogeHoge =44; 
b; 
fafweafwa; 
hoge; 
HogeHoge;";
        Assert.True(GetAnalyzer(code).Analyze());

        // 存在しない名前の参照
        string code2 = @"
aiueo;
bfaffaf;
cfwefeeee;
ffafwsfa;";
        Assert.False(GetAnalyzer(code2).Analyze());
    }

    [Fact]
    public void AnalyzeInfixExpressionTest()
    {
        string code = @"
333443 + 443434;
44343 + 34444";

        Assert.True(GetAnalyzer(code).Analyze());

        string code2 = @"
333443 + true;
44343 + false";

        Assert.False(GetAnalyzer(code2).Analyze());
    }

    [Fact]
    public void AnalyzePrefixExpressionTest()
    {
        string code = @"
-12;
-43;";

        Assert.True(GetAnalyzer(code).Analyze());
    }

    private Analyzer GetAnalyzer(string code)
    {
        Lexer lexer = new Lexer(code);
        Errors errors = new Errors();
        Parser parser = new Parser(lexer,errors, Console.WriteLine);
        Analyzer analyzer = new Analyzer(parser.Parse());
        return analyzer;
    }
}
