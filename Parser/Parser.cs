using sena.AST;
using sena.AST.Expressions;
using sena.AST.Statements;
using sena.Lexing;
using System.Collections.ObjectModel;

namespace sena.Parsing;

using PrefixParseFunction = Func<IExpression>;
using InfixParseFunction = Func<IExpression, IExpression>;

public class Parser
{
    public Token currentToken { get; private set; }
    public Token nextToken { get; private set; }
    public readonly ReadOnlyDictionary<TokenType, PrefixParseFunction> prefixParseFunctions;
    public readonly ReadOnlyDictionary<TokenType, InfixParseFunction> infixParseFunctions;
    Lexer lexer;

    public Parser(Lexer lexer)
    {
        this.lexer = lexer;

        currentToken = lexer.NextToken();
        nextToken = lexer.NextToken();
        prefixParseFunctions = RegisterPrefixParseFunctions().AsReadOnly();
        infixParseFunctions = RegisterInfixParseFunctions().AsReadOnly();
    }

    private Dictionary<TokenType, PrefixParseFunction> RegisterPrefixParseFunctions()
    {
        return new Dictionary<TokenType, PrefixParseFunction>()
        {

        };
    }

    private Dictionary<TokenType, InfixParseFunction> RegisterInfixParseFunctions()
    {
        return new Dictionary<TokenType, InfixParseFunction>()
        {

        };
    }

    private void ReadToken()
    {
        currentToken = nextToken;
        nextToken = lexer.NextToken();
    }

    private bool ExpectPeek(TokenType type)
    {
        if (nextToken.tokenType == type)
        {
            ReadToken();
            return true;
        }

        return false;
    }

    private IStatement ParseStatement()
    {
        switch (currentToken.tokenType)
        {
            case TokenType.LET:
                return ParseLetStatement();
            default:
                return null;
        }
    }

    private IExpression ParseExpression()
    {
        return null;
    }

    private LetStatement? ParseLetStatement()
    {
        // current = let

        Identifier identifier;
        IExpression expression = null;

        // name
        if (!ExpectPeek(TokenType.IDENTIFIER)) return null;
        identifier = new Identifier(currentToken, currentToken.literal);

        // =
        if (!ExpectPeek(TokenType.EQ)) return null;

        // expression
        expression = ParseExpression();

        return new LetStatement(identifier, expression);
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
