using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace QowaivAnalyzer.Rules.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class UseInjectableDateTimeProvider: UseInjectableDateTimeProviderBase<SyntaxKind>
    {
        protected override SyntaxKind SyntaxKind => SyntaxKind.IdentifierName;

        protected override void CheckNode(SyntaxNodeAnalysisContext context)
        {
            var identifier = ((IdentifierNameSyntax)context.Node)?.Identifier;
            CheckNode(context, identifier.GetValueOrDefault());
        }
    }
}
