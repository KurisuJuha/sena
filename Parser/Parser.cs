using sena.Lexing;

namespace sena.Parsing;

public class Parser
{
    Lexer lexer;

    public Parser(Lexer lexer)
    {
        this.lexer = lexer;
    }
}
