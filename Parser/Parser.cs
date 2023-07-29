using System.Collections.ObjectModel;
using sena.AST;
using sena.AST.Expressions;
using sena.AST.Statements;
using sena.Lexing;

namespace sena.Parsing;

using PrefixParseFunction = Func<IExpression?>;
using InfixParseFunction = Func<IExpression, IExpression?>;

public sealed class Parser
{
    private readonly Errors _errors;
    private readonly ReadOnlyDictionary<TokenType, InfixParseFunction> _infixParseFunctions;
    private readonly Lexer _lexer;
    private readonly ReadOnlyDictionary<TokenType, Precedence> _precedences;
    private readonly ReadOnlyDictionary<TokenType, PrefixParseFunction> _prefixParseFunctions;

    public Parser(Lexer lexer, Errors errors, Action<string>? log = null)
    {
        _lexer = lexer;
        _errors = errors;
        Log = Console.WriteLine;
        if (log != null) Log = log;
        _prefixParseFunctions = RegisterPrefixParseFunctions().AsReadOnly();
        _infixParseFunctions = RegisterInfixParseFunctions().AsReadOnly();
        _precedences = RegisterPrecedences().AsReadOnly();

        CurrentToken = lexer.NextToken();
        NextToken = lexer.NextToken();
    }

    private Token CurrentToken { get; set; }
    private Token NextToken { get; set; }

    private Precedence CurrentPrecedence =>
        _precedences.TryGetValue(CurrentToken.TokenType, out var value) ? value : Precedence.Lowest;

    private Precedence NextPrecedence =>
        _precedences.TryGetValue(NextToken.TokenType, out var value) ? value : Precedence.Lowest;

    private event Action<string> Log;

    public Root Parse()
    {
        var statements = new List<IStatement>();

        while (CurrentToken.TokenType != TokenType.Eof)
        {
            var statement = ParseStatement();
            if (statement != null)
                statements.Add(statement);
            else
                ReadToken();
        }

        return new Root(statements);
    }

    private Dictionary<TokenType, Precedence> RegisterPrecedences()
    {
        return new Dictionary<TokenType, Precedence>
        {
            [TokenType.Plus] = Precedence.Sum,
            [TokenType.Minus] = Precedence.Sum,
            [TokenType.Asterisk] = Precedence.Product,
            [TokenType.Slash] = Precedence.Product
        };
    }

    private Dictionary<TokenType, PrefixParseFunction> RegisterPrefixParseFunctions()
    {
        return new Dictionary<TokenType, PrefixParseFunction>
        {
            [TokenType.Identifier] = ParseIdentifier,
            [TokenType.IntegerLiteral] = ParseIntLiteral,
            [TokenType.Minus] = ParsePrefixExpression,
            [TokenType.Lparen] = ParseGroupedExpression,
            [TokenType.True] = ParseBoolLiteral,
            [TokenType.False] = ParseBoolLiteral
        };
    }

    private Dictionary<TokenType, InfixParseFunction> RegisterInfixParseFunctions()
    {
        return new Dictionary<TokenType, InfixParseFunction>
        {
            [TokenType.Plus] = ParseInfixExpression,
            [TokenType.Minus] = ParseInfixExpression,
            [TokenType.Asterisk] = ParseInfixExpression,
            [TokenType.Slash] = ParseInfixExpression
        };
    }

    private void ReadToken()
    {
        CurrentToken = NextToken;
        NextToken = _lexer.NextToken();
    }

    private bool ExpectCurrent(TokenType tokenType)
    {
        if (CurrentToken.TokenType == tokenType)
        {
            ReadToken();
            return true;
        }

        _errors.AddError($"{CurrentToken.TokenType} ではなく {tokenType} である必要があります。");
        return false;
    }

    private IStatement? ParseStatement()
    {
        switch (CurrentToken.TokenType)
        {
            case TokenType.LetKeyword:
                return ParseLetStatement();
            case TokenType.ReturnKeyword:
                return ParseReturnStatement();
            case TokenType.Illegal:
            case TokenType.Eof:
            case TokenType.Identifier:
            case TokenType.IntegerLiteral:
            case TokenType.Lparen:
            case TokenType.Rparen:
            case TokenType.Semicolon:
            case TokenType.Assign:
            case TokenType.Minus:
            case TokenType.Plus:
            case TokenType.Asterisk:
            case TokenType.Slash:
            case TokenType.True:
            case TokenType.False:
            default:
                var expressionStatement = ParseExpressionStatement();
                if (expressionStatement != null) return expressionStatement;

                _errors.AddError(CurrentToken.TokenType + " から始まる文は存在しません。");
                return null;
        }
    }

    private IExpression? ParseExpression(Precedence precedence)
    {
        // 前置
        _prefixParseFunctions.TryGetValue(CurrentToken.TokenType, out var prefix);
        if (prefix == null)
        {
            _errors.AddError($"{CurrentToken.TokenType} から始まる PrefixParseFunction はありません。");
            return null;
        }

        var leftExpression = prefix();

        // 中置
        while (precedence < CurrentPrecedence)
        {
            _infixParseFunctions.TryGetValue(CurrentToken.TokenType, out var infix);
            if (infix == null) return leftExpression;

            if (leftExpression == null) return null;
            leftExpression = infix(leftExpression);
        }

        return leftExpression;
    }

    private void OnLog(string obj)
    {
        Log?.Invoke(obj);
    }

    #region ParseStatements

    private LetStatement? ParseLetStatement()
    {
        // let
        if (!ExpectCurrent(TokenType.LetKeyword)) return null;

        // identifier
        var name = ParseIdentifier();

        // =
        if (!ExpectCurrent(TokenType.Assign)) return null;

        // value
        var value = ParseExpression(Precedence.Lowest);

        // ;
        if (!ExpectCurrent(TokenType.Semicolon)) return null;

        if (name == null) return null;
        if (value == null) return null;

        return new LetStatement(name, value);
    }

    private ExpressionStatement? ParseExpressionStatement()
    {
        // 式
        var expression = ParseExpression(Precedence.Lowest);
        if (expression == null) return null;

        // ;
        return !ExpectCurrent(TokenType.Semicolon) ? null : new ExpressionStatement(expression);
    }

    private ReturnStatement? ParseReturnStatement()
    {
        // return
        if (!ExpectCurrent(TokenType.ReturnKeyword)) return null;

        // 式
        var expression = ParseExpression(Precedence.Lowest);
        if (expression == null) return null;

        // ;
        if (!ExpectCurrent(TokenType.Semicolon)) return null;

        return new ReturnStatement(expression);
    }

    #endregion

    #region ParseExpressions

    private Identifier? ParseIdentifier()
    {
        if (CurrentToken.TokenType != TokenType.Identifier) return null;
        var identifier = new Identifier(CurrentToken.Literal);
        ReadToken();
        return identifier;
    }

    private IntLiteral? ParseIntLiteral()
    {
        if (CurrentToken.TokenType != TokenType.IntegerLiteral) return null;
        var intLiteral = new IntLiteral(CurrentToken.Literal);
        ReadToken();
        return intLiteral;
    }

    private BoolLiteral ParseBoolLiteral()
    {
        var boolLiteral = new BoolLiteral(CurrentToken.TokenType == TokenType.True);
        ReadToken();

        return boolLiteral;
    }

    private PrefixExpression? ParsePrefixExpression()
    {
        var op = CurrentToken.Literal;

        ReadToken();

        var expression = ParseExpression(Precedence.Prefix);
        return expression == null ? null : new PrefixExpression(op, expression);
    }

    private InfixExpression? ParseInfixExpression(IExpression leftExpression)
    {
        var op = CurrentToken.Literal;
        var precedence = CurrentPrecedence;
        ReadToken();
        var rightExpression = ParseExpression(precedence);
        return rightExpression == null ? null : new InfixExpression(op, rightExpression, leftExpression);
    }

    private IExpression? ParseGroupedExpression()
    {
        if (!ExpectCurrent(TokenType.Lparen)) return null;

        var expression = ParseExpression(Precedence.Lowest);

        return !ExpectCurrent(TokenType.Rparen) ? null : expression;
    }

    #endregion
}