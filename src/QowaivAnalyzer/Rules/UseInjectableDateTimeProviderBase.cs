using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace QowaivAnalyzer.Rules
{
    public abstract class UseInjectableDateTimeProviderBase<SyntaxKindType> : DiagnosticAnalyzer
        where SyntaxKindType : struct
    {
        internal readonly DiagnosticDescriptor rule = RuleDescription.Build(
            DiagnosticsId.UseInjectableDateTimeProvider,
            "Use an injectable DateTime provider",
            null,
            DiagnosticsCategory.Testability);

        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(rule);

        public sealed override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(CheckNode, SyntaxKind);

        protected abstract SyntaxKindType SyntaxKind { get; }

        protected abstract void CheckNode(SyntaxNodeAnalysisContext context);

        protected void CheckNode(SyntaxNodeAnalysisContext context, SyntaxToken identifier)
        {
            var name = identifier.ValueText;

            if (CandidateNames.Contains(name) &&
                context.SemanticModel.GetSymbolInfo(context.Node).Symbol is IPropertySymbol symbol &&
                IsCandidateType(symbol.ContainingType))
            {
                var diagnostic = Diagnostic.Create(rule, context.Node.Parent.GetLocation(), name);
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static bool IsCandidateType(ITypeSymbol symbol)
        {
            return KnownType.System_DateTime.Matches(symbol)
                || KnownType.Microsoft_VisualBasic_DateAndTime.Matches(symbol);
        }

        private readonly HashSet<string> CandidateNames = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
        {
            nameof(DateTime.Now),
            nameof(DateTime.Today),
            nameof(DateTime.UtcNow),
        };
    }
}
