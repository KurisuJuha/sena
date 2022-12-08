using sena.AST;
using sena.AST.Expressions;
using sena.AST.Statements;
using sena.Lexing;
using System.Collections.ObjectModel;

namespace sena.Parsing;

using PrefixParseFunction = Func<IExpression?>;
using InfixParseFunction = Func<IExpression?, IExpression>;

public class Parser
{
    public Token currentToken { get; private set; }
    public Token nextToken { get; private set; }
    public readonly ReadOnlyDictionary<TokenType, PrefixParseFunction> prefixParseFunctions;
    public readonly ReadOnlyDictionary<TokenType, InfixParseFunction> infixParseFunctions;
    Lexer lexer;
    Errors errors;
    event Action<string> Log;

    public Parser(Lexer lexer, Errors errors, Action<string>? Log = null)
    {
        this.lexer = lexer;
        this.errors = errors;
        this.Log = Console.WriteLine;
        if (Log != null) this.Log = Log;
        prefixParseFunctions = RegisterPrefixParseFunctions().AsReadOnly();
        infixParseFunctions = RegisterInfixParseFunctions().AsReadOnly();

        currentToken = lexer.NextToken();
        nextToken = lexer.NextToken();
    }

    private Dictionary<TokenType, PrefixParseFunction> RegisterPrefixParseFunctions()
    {
        return new Dictionary<TokenType, PrefixParseFunction>()
        {
            [TokenType.IDENTIFIER] = ParseIdentifier,
            [TokenType.INTEGER_LITERAL] = ParseIntLiteral,
            [TokenType.MINUS] = ParsePrefixExpression,
            [TokenType.PLUS] = ParsePrefixExpression,
            [TokenType.BANG] = ParsePrefixExpression,
        };
    }

    private Dictionary<TokenType, InfixParseFunction> RegisterInfixParseFunctions()
    {
        return new Dictionary<TokenType, InfixParseFunction>()
        {

        };
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

            ReadToken();
        }

        return new Root(statements);
    }

    private void ReadToken()
    {
        currentToken = nextToken;
        nextToken = lexer.NextToken();
    }

    /// <summary>
    /// 次のトークンが指定したTokenTypeと同じなら読み進める。違う場合はエラーとして出力する。
    /// </summary>
    private bool ExpectPeek(TokenType type)
    {
        if (nextToken.tokenType == type)
        {
            ReadToken();
            return true;
        }

        errors.AddError(nextToken.tokenType + " ではなく、 " + type + " である必要があります。");
        return false;
    }

    private IStatement? ParseStatement()
    {
        switch (currentToken.tokenType)
        {
            case TokenType.RETURN:
                return ParseReturnStatement();
            default:
                ExpressionStatement? expressionStatement = ParseExpressionStatement();
                if (expressionStatement != null) return expressionStatement;

                errors.AddError(currentToken.tokenType + " から始まる文は存在しません。");
                return null;
        }
    }

    private IExpression? ParseExpression()
    {
        prefixParseFunctions.TryGetValue(currentToken.tokenType, out var prefix);
        if (prefix == null)
        {
            errors.AddError($"{currentToken.tokenType} に関連付けられているprefixParseFunctionはありません。");
            return null;
        }

        IExpression? leftExpression = prefix();

        return leftExpression;
    }

    #region ParseStatements
    private ReturnStatement? ParseReturnStatement()
    {
        // returnを飛ばす
        ReadToken();

        //TODO : Expression部分
        IExpression? expression = ParseExpression();

        if (expression == null) return null;
        return new ReturnStatement(expression);
    }

    private ExpressionStatement? ParseExpressionStatement()
    {
        IExpression? expression = ParseExpression();

        if (expression == null) return null;

        return new ExpressionStatement(expression);
    }
    #endregion

    #region ParseExpressions
    private Identifier? ParseIdentifier()
    {
        // 識別子
        string name = currentToken.literal;
        ReadToken();
        return new Identifier(name);
    }

    private IntLiteral? ParseIntLiteral()
    {
        // int
        string value = currentToken.literal;
        ReadToken();
        return new IntLiteral(value);
    }

    private PrefixExpression? ParsePrefixExpression()
    {
        string op = currentToken.literal;

        // opを飛ばす
        ReadToken();

        IExpression? leftExpression = ParseExpression();
        if (leftExpression == null) return null;

        return new PrefixExpression(op, leftExpression);
    }
    #endregion
}