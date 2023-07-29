namespace sena.Parsing;

public class Errors
{
    private readonly List<string> _errorList = new();
    public IReadOnlyCollection<string> ErrorList => _errorList;

    public void AddError(string error)
    {
        _errorList.Add(error);
    }

    public void WriteLine(Action<string> write)
    {
        foreach (var error in _errorList) write(error);
    }
}