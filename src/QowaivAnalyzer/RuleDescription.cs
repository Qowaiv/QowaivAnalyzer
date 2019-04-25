using Microsoft.CodeAnalysis;

namespace QowaivAnalyzer
{
    public static class RuleDescription
    {
        public static DiagnosticDescriptor Build(
            DiagnosticsId id,
            string title,
            string messageFormat,
            DiagnosticsCategory category,
            DiagnosticSeverity severity = DiagnosticSeverity.Warning,
            bool isEnabledByDefault = true
        )
        {
            return new DiagnosticDescriptor
            (
                id: $"QOWAIV{(int)id:0000}",
                title: title,
                messageFormat: messageFormat ?? title,
                category: category.ToString(),
                defaultSeverity: severity,
                isEnabledByDefault: isEnabledByDefault
            );
        }
    }
}
