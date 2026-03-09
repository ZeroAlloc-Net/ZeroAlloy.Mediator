using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace ZMediator.Tests.GeneratorTests;

public class DiagnosticTests
{
    [Fact]
    public void ZM002_DuplicateHandler_EmitsError()
    {
        var source = """
            using ZMediator;
            using System.Threading;
            using System.Threading.Tasks;

            namespace TestApp;

            public readonly record struct Ping : IRequest<string>;

            public class PingHandler1 : IRequestHandler<Ping, string>
            {
                public ValueTask<string> Handle(Ping request, CancellationToken ct)
                    => ValueTask.FromResult("Pong1");
            }

            public class PingHandler2 : IRequestHandler<Ping, string>
            {
                public ValueTask<string> Handle(Ping request, CancellationToken ct)
                    => ValueTask.FromResult("Pong2");
            }
            """;

        var (_, diagnostics) = GeneratorTestHelper.RunGenerator(source);

        var zm002 = diagnostics.FirstOrDefault(d => d.Id == "ZM002");
        Assert.NotNull(zm002);
        Assert.Equal(DiagnosticSeverity.Error, zm002.Severity);
    }

    [Fact]
    public void ZM003_ClassRequest_EmitsWarning()
    {
        var source = """
            using ZMediator;
            using System.Threading;
            using System.Threading.Tasks;

            namespace TestApp;

            public class Ping : IRequest<string> { }

            public class PingHandler : IRequestHandler<Ping, string>
            {
                public ValueTask<string> Handle(Ping request, CancellationToken ct)
                    => ValueTask.FromResult("Pong");
            }
            """;

        var (_, diagnostics) = GeneratorTestHelper.RunGenerator(source);

        var zm003 = diagnostics.FirstOrDefault(d => d.Id == "ZM003");
        Assert.NotNull(zm003);
        Assert.Equal(DiagnosticSeverity.Warning, zm003.Severity);
    }
}
