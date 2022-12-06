# sena

sena is a programming language.

```sena fibonacci.sn
Console.WriteLine(fib(20));

func int fib(int n)
{
    mutable int a = 0;
    mutable int b = 1;

    for (int i = 0; i < n; i++ )
    {
        mutable int c = a + b;
        a = b;
        b = c;
    }

    return a;
}
```