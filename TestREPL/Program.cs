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
            Console.WriteLine(code);
            Lexer lexer = new Lexer(code);

            Token token = lexer.NextToken();
            while (token.tokenType != TokenType.EOF)
            {
                Console.WriteLine("literal : " + token.literal + " type : " + token.tokenType);
                token = lexer.NextToken();
            }

            Lexer lexer2 = new Lexer(code);
            Errors errors = new Errors();
            Parser parser = new Parser(lexer, errors);
            Root root = parser.Parse();

            Console.WriteLine(root.ToCode());
            foreach (var error in errors.errors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ResetColor();
            }
        }
    }
}