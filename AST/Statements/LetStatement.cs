using sena.AST.Expressions;

namespace sena.AST.Statements;

public class LetStatement : IStatement
{
    public readonly Identifier Identifier;
    public readonly IExpression Value;

    public LetStatement(Identifier identifier, IExpression value)
    {
        Identifier = identifier;
        Value = value;
    }

    public string ToCode()
    {
        return $"let {Identifier.ToCode()} = {Value.ToCode()};";
    }
}