using sena.AST;
using sena.Lexing;

namespace sena.Parsing;

using PrefixParseFunction = Func<IExpression?>;
using InfixParseFunction = Func<IExpression, IExpression?>;

public class Parser
{
    public Token currentToken { get; private set; }
    public Token nextToken { get; private set; }
    readonly Lexer lexer;
    readonly Errors errors;
    event Action<string> Log;

    public Parser(Lexer lexer, Errors errors, Action<string>? Log = null)
    {
        this.lexer = lexer;
        this.errors = errors;
        this.Log = Console.WriteLine;
        if (Log != null) this.Log = Log;

        currentToken = lexer.NextToken();
        nextToken = lexer.NextToken();
    }
    public Root Parse()
    {
        List<IStatement> statements = new List<IStatement>();

        while (currentToken.tokenType != TokenType.EOF)
        {
            IStatement? statement = ParseStatement();
            if (statement != null)
            {
                statements.Add(statement);
            }
            else
            {
                ReadToken();
            }
        }

        return new Root(statements);
    }

    private void ReadToken()
    {
        currentToken = nextToken;
        nextToken = lexer.NextToken();
    }

    private IStatement? ParseStatement()
    {
        switch (currentToken.tokenType)
        {
            default:
                errors.AddError(currentToken.tokenType + " から始まる文は存在しません。");
                return null;
        }
    }

    #region ParseStatements
    #endregion

    #region ParseExpressions
    #endregion
}