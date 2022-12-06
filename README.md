# sena

sena is a programming language.

```sena fibonacci.sn
Console.WriteLine(fib(20));

function int Fibonacci(int n)
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

```sena FizzBuzz.sn
FizzBuzz(100);

function int FizzBuzz(int n)
{
    for (int i = 0; i < n; i++)
    {
        if (n % 15 == 0)
        {
            Console.WriteLine("FizzBuzz");
        }elif(n % 3 == 0)
        {
            Console.WriteLine("Fizz");
        }elif(n % 5 == 0)
        {
            Console.WriteLine("Buzz");
        }else
        {
            Console.WriteLine(n);
        }
    }
}
```
