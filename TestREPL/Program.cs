using sena.Lexing;

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
        }
    }
}