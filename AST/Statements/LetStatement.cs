using sena.AST.Expressions;

namespace sena.AST.Statements;

public class LetStatement : IStatement
{
    public readonly Identifier name;
    public readonly IExpression value;

    public LetStatement(Identifier name, IExpression value)
    {
        this.name = name;
        this.value = value;
    }
}
