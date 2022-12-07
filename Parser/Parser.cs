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

    private IStatement ParseStatement()
    {
        return null;
    }

    public Root Parse()
    {
        List<IStatement> statements = new List<IStatement>();

        while (currentToken.tokenType != TokenType.EOF)
        {
            IStatement statement = ParseStatement();

            if (statement != null)
            {
                statements.Add(statement);
            }

            ReadToken();
        }

        return new Root(statements);
    }
}
