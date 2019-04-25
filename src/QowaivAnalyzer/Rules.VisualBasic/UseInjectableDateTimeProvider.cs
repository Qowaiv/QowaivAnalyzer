using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace QowaivAnalyzer.Rules.VisualBasic
{
    [DiagnosticAnalyzer(LanguageNames.VisualBasic)]
    public sealed class UseInjectableDateTimeProvider : UseInjectableDateTimeProviderBase<SyntaxKind>
    {
        protected override SyntaxKind SyntaxKind => SyntaxKind.IdentifierName;

        protected override void CheckNode(SyntaxNodeAnalysisContext context)
        {
            var identifier = ((IdentifierNameSyntax)context.Node)?.Identifier;
            CheckNode(context, identifier.GetValueOrDefault());
        }
    }
}
