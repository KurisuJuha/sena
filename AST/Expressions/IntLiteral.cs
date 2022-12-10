namespace sena.AST.Expressions
{
    public class IntLiteral : IExpression
    {
        public readonly string value;

        public IntLiteral(string value)
        {
            this.value = value;
        }

        public string ToCode()
        {
            return value;
        }
    }
}
