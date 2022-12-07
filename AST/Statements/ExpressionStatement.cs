namespace sena.AST.Statements;

public class ExpressionStatement : IStatement
{
    public readonly IExpression expression;

    public ExpressionStatement(IExpression expression)
    {
        this.expression = expression;
    }
}
