using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace ZMediator.Tests.GeneratorTests;

public class ConfigurationGeneratorTests
{
    [Fact]
    public void Generator_EmitsConfigureMethod()
    {
        var source = """
            using ZMediator;
            using System.Threading;
            using System.Threading.Tasks;

            namespace TestApp;

            public readonly record struct Ping(string Message) : IRequest<string>;

            public class PingHandler : IRequestHandler<Ping, string>
            {
                public ValueTask<string> Handle(Ping request, CancellationToken ct)
                    => ValueTask.FromResult("Pong");
            }
            """;

        var (output, diagnostics) = GeneratorTestHelper.RunGenerator(source);

        Assert.Empty(diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
        Assert.Contains("public static void Configure(", output);
        Assert.Contains("MediatorConfig", output);
        Assert.Contains("SetFactory", output);
    }
}
