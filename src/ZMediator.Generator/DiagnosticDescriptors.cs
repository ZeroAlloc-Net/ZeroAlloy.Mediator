#nullable enable
using Microsoft.CodeAnalysis;

namespace ZMediator.Generator
{
    internal static class DiagnosticDescriptors
    {
        public static readonly DiagnosticDescriptor DuplicateHandler = new DiagnosticDescriptor(
            "ZM002",
            "Duplicate request handler",
            "Request type '{0}' has multiple handlers: {1}",
            "ZMediator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static readonly DiagnosticDescriptor ClassRequest = new DiagnosticDescriptor(
            "ZM003",
            "Request type is a class",
            "Request type '{0}' is a class; use 'readonly record struct' for zero-allocation dispatch.",
            "ZMediator",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
    }
}
