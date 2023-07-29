using System.Collections.ObjectModel;

namespace sena.AST;

public class Root : INode
{
    public readonly ReadOnlyCollection<IStatement> Statements;

    public Root(List<IStatement> statements)
    {
        Statements = statements.AsReadOnly();
    }

    public string ToCode()
    {
        return string.Join('\n', Statements.Select(s => s.ToCode()));
    }
}