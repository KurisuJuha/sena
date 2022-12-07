using sena.AST;
using sena.Lexing;

namespace sena.Parsing;

public class Parser
{
    public Token currentToken { get; private set; }
    public Token nextToken { get; private set; }
    Lexer lexer;

    public Parser(Lexer lexer)
    {
        this.lexer = lexer;

        currentToken = lexer.NextToken();
        nextToken = lexer.NextToken();
    }

    private void ReadToken()
    {
        currentToken = nextToken;
        nextToken = lexer.NextToken();
    }

    public Root Parse()
    {
        return null;
    }
}
