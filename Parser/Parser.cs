using sena.AST;
using sena.AST.Expressions;
using sena.AST.Statements;
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
            case TokenType.LET_KEYWORD:
                return ParseLetStatement();
            default:
                errors.AddError(currentToken.tokenType + " から始まる文は存在しません。");
                return null;
        }
    }

    private IExpression? ParseExpression()
    {
        ReadToken();
        return new Identifier("test");
    }

    /// <summary>
    /// 現在のtokentypeが期待しているtokentypeならReadTokenする。
    /// </summary>
    private bool ExpectCurrent(TokenType tokenType)
    {
        if (currentToken.tokenType == tokenType)
        {
            ReadToken();
            return true;
        }

        errors.AddError($"{currentToken.tokenType} ではなく {tokenType} である必要があります。");
        return false;
    }

    #region ParseStatements
    private LetStatement? ParseLetStatement()
    {
        // let
        if (!ExpectCurrent(TokenType.LET_KEYWORD)) return null;

        // identifier
        Identifier? name = ParseIdentifier();

        // =
        if (!ExpectCurrent(TokenType.ASSIGN)) return null;

        // value
        IExpression? value = ParseExpression();

        // ;
        ReadToken();

        if (name == null) return null;
        if (value == null) return null;

        return new LetStatement(name, value);
    }
    #endregion

    #region ParseExpressions
    private Identifier? ParseIdentifier()
    {
        if (currentToken.tokenType != TokenType.IDENTIFIER) return null;
        Identifier identifier = new Identifier(currentToken.literal);
        ReadToken();
        return identifier;
    }
    #endregion
}