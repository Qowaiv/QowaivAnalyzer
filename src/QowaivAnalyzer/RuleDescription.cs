using Microsoft.CodeAnalysis;

namespace QowaivAnalyzer
{
    public static class RuleDescription
    {
        public static DiagnosticDescriptor Build(
            string id,
            string title,
            string messageFormat,
            DiagnosticsCategory category,
            DiagnosticSeverity severity = DiagnosticSeverity.Warning,
            bool isEnabledByDefault = true
        )
        {
            return new DiagnosticDescriptor(id, title, messageFormat ?? title, category.ToString(), severity, isEnabledByDefault);
        }
    }
}
