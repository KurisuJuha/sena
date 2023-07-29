namespace sena.Parsing;

public enum Precedence
{
    Lowest = 1,
    Equal, // ==
    LessGreater, // ><
    Sum, // +-
    Product, // */
    Prefix, // -x !x
    Call // hoge()
}