using Xunit.Abstractions;

namespace sena.Test;

public class LexerTest
{
    private readonly ITestOutputHelper Console;
    public LexerTest(ITestOutputHelper testOutputHelper)
    {
        Console = testOutputHelper;
    }
}
