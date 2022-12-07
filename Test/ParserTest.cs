using sena.AST;
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
}
