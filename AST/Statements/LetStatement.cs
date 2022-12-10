using sena.AST.Expressions;

namespace sena.AST.Statements;

public class LetStatement : IStatement
{
    public readonly Identifier identifier;
    public readonly IExpression value;

    public LetStatement(Identifier identifier,IExpression value)
    {
        this.identifier = identifier;
        this.value = value;
    }

    public string ToCode()
    {
        return $"let {identifier.ToCode()} = {value.ToCode()};";
    }
}
