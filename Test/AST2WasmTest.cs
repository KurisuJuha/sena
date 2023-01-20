using sena.Wasm;
using sena.AST;
using Xunit.Abstractions;

namespace sena.Test;

public class AST2WasmTest
{
    private readonly ITestOutputHelper Console;
    public AST2WasmTest(ITestOutputHelper testOutputHelper)
    {
        Console = testOutputHelper;
    }

    [Fact]
    public void RootNodeTest()
    {
        AST2Wasm ast2Wasm = new AST2Wasm(new Root(new List<IStatement>()
        {
        }));

        string wasm = ast2Wasm.Compile();
        Assert.Equal("(module(memory 1)(export \"memory\" (memory 0)))", wasm);
    }
}