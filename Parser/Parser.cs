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

        while (CurrentToken.TokenType != TokenType.EOF)
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
            [TokenType.PLUS] = Precedence.Sum,
            [TokenType.MINUS] = Precedence.Sum,
            [TokenType.ASTERISK] = Precedence.Product,
            [TokenType.SLASH] = Precedence.Product
        };
    }

    private Dictionary<TokenType, PrefixParseFunction> RegisterPrefixParseFunctions()
    {
        return new Dictionary<TokenType, PrefixParseFunction>
        {
            [TokenType.IDENTIFIER] = ParseIdentifier,
            [TokenType.INTEGER_LITERAL] = ParseIntLiteral,
            [TokenType.MINUS] = ParsePrefixExpression,
            [TokenType.LPAREN] = ParseGroupedExpression,
            [TokenType.TRUE] = ParseBoolLiteral,
            [TokenType.FALSE] = ParseBoolLiteral
        };
    }

    private Dictionary<TokenType, InfixParseFunction> RegisterInfixParseFunctions()
    {
        return new Dictionary<TokenType, InfixParseFunction>
        {
            [TokenType.PLUS] = ParseInfixExpression,
            [TokenType.MINUS] = ParseInfixExpression,
            [TokenType.ASTERISK] = ParseInfixExpression,
            [TokenType.SLASH] = ParseInfixExpression
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
            case TokenType.LET_KEYWORD:
                return ParseLetStatement();
            case TokenType.RETURN_KEYWORD:
                return ParseReturnStatement();
            case TokenType.ILLEGAL:
            case TokenType.EOF:
            case TokenType.IDENTIFIER:
            case TokenType.INTEGER_LITERAL:
            case TokenType.LPAREN:
            case TokenType.RPAREN:
            case TokenType.SEMICOLON:
            case TokenType.ASSIGN:
            case TokenType.MINUS:
            case TokenType.PLUS:
            case TokenType.ASTERISK:
            case TokenType.SLASH:
            case TokenType.TRUE:
            case TokenType.FALSE:
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
        if (!ExpectCurrent(TokenType.LET_KEYWORD)) return null;

        // identifier
        var name = ParseIdentifier();

        // =
        if (!ExpectCurrent(TokenType.ASSIGN)) return null;

        // value
        var value = ParseExpression(Precedence.Lowest);

        // ;
        if (!ExpectCurrent(TokenType.SEMICOLON)) return null;

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
        return !ExpectCurrent(TokenType.SEMICOLON) ? null : new ExpressionStatement(expression);
    }

    private ReturnStatement? ParseReturnStatement()
    {
        // return
        if (!ExpectCurrent(TokenType.RETURN_KEYWORD)) return null;

        // 式
        var expression = ParseExpression(Precedence.Lowest);
        if (expression == null) return null;

        // ;
        if (!ExpectCurrent(TokenType.SEMICOLON)) return null;

        return new ReturnStatement(expression);
    }

    #endregion

    #region ParseExpressions

    private Identifier? ParseIdentifier()
    {
        if (CurrentToken.TokenType != TokenType.IDENTIFIER) return null;
        var identifier = new Identifier(CurrentToken.Literal);
        ReadToken();
        return identifier;
    }

    private IntLiteral? ParseIntLiteral()
    {
        if (CurrentToken.TokenType != TokenType.INTEGER_LITERAL) return null;
        var intLiteral = new IntLiteral(CurrentToken.Literal);
        ReadToken();
        return intLiteral;
    }

    private BoolLiteral ParseBoolLiteral()
    {
        var boolLiteral = new BoolLiteral(CurrentToken.TokenType == TokenType.TRUE);
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
        if (!ExpectCurrent(TokenType.LPAREN)) return null;

        var expression = ParseExpression(Precedence.Lowest);

        return !ExpectCurrent(TokenType.RPAREN) ? null : expression;
    }

    #endregion
}