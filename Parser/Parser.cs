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
    Errors errors;

    public Parser(Lexer lexer, Errors errors)
    {
        this.lexer = lexer;
        this.errors = errors;
        prefixParseFunctions = RegisterPrefixParseFunctions().AsReadOnly();
        infixParseFunctions = RegisterInfixParseFunctions().AsReadOnly();

        currentToken = lexer.NextToken();
        nextToken = lexer.NextToken();
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
                errors.AddError(currentToken.tokenType + " から始まる文は存在しません。");
                return null;
        }
    }

    private IStatement? ParseReturnStatement()
    {
        // returnを飛ばす
        ReadToken(); 

        //TODO : Expression部分
        while (currentToken.tokenType != TokenType.SEMICOLON)
        {
            ReadToken();
        }

        // ;
        if (currentToken.tokenType != TokenType.SEMICOLON) return null;

        return new ReturnStatement(null);
    }
}
