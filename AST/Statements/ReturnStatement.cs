namespace sena.AST.Statements;

public class ReturnStatement : IStatement
{
    private readonly IExpression _expression;

    public ReturnStatement(IExpression expression)
    {
        _expression = expression;
    }

    public string ToCode()
    {
        return $"return {_expression.ToCode()};";
    }
}