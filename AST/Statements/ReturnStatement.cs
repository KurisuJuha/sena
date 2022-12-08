namespace sena.AST.Statements;

public class ReturnStatement : IStatement
{
    public readonly IExpression expression;

    public ReturnStatement(IExpression expression)
    {
        this.expression = expression;
    }

    public string ToCode()
    {
        return "return " + expression.ToCode() + ";";
    }
}
