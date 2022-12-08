namespace sena.Parsing
{
    public class Errors
    {
        private List<string> _errors = new List<string>();
        public IReadOnlyCollection<string> errors => _errors;

        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}
