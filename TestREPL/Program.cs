using sena.AST;
using sena.Lexing;
using sena.Parsing;

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

            errors.WriteLine(Console.WriteLine);
            Console.WriteLine(root.ToCode());
        }
    }
}
