using sena.AST;
using sena.Lexing;
using sena.Parsing;
using sena.Analyzing;

namespace sena.TestREPL;

internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write(">>>");
            string code = Console.ReadLine() ?? "";
            Lexer lexer = new Lexer(code);
            Errors errors = new Errors();
            Parser parser = new Parser(lexer, errors, Console.WriteLine);
            Root root = parser.Parse();
            Analyzer analyzer = new Analyzer(root);

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            errors.WriteLine(Console.WriteLine);
            Console.ResetColor();
            Console.WriteLine(root.ToCode());
            Console.Write("analyzing: ");
            bool analyzeR = analyzer.Analyze();
            if (analyzeR) Console.ForegroundColor = ConsoleColor.Blue;
            else Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(analyzeR);
            Console.ResetColor();
            Console.WriteLine("");
        }
    }
}
