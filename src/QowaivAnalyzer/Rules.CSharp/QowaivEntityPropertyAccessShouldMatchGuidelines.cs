using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace QowaivAnalyzer.Rules.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class QowaivEntityPropertyAccessShouldMatchGuidelines : DiagnosticAnalyzer
    {
        private readonly DiagnosticDescriptor rule = RuleDescription.Build(
           DiagnosticsId.QowaivEntityPropertyAccessShouldMatchGuidelines,
           "The access to a property of an Qowaiv.DomainModel.Entity should follow the guidelines.",
           null,
           DiagnosticsCategory.Design);

        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(rule);

        public sealed override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(CheckNode, SyntaxKind.PropertyDeclaration);

        private void CheckNode(SyntaxNodeAnalysisContext context)
        {
            var node = (PropertyDeclarationSyntax)context.Node;

            if (NaN(context, node)) { return; }

            var getter = node.AccessorList.Accessors.ToArray();

            var diagnostic = Diagnostic.Create(rule, context.Node.Parent.GetLocation());
            context.ReportDiagnostic(diagnostic);
        }

        private bool NaN(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax node)
        {
            if(node.AccessorList is null)
            {
                return true;
            }
            var type = context.SemanticModel.GetDeclaredSymbol(node).ContainingType;
            return !type.DerivesFrom(KnownType.Qowaiv_DomainModel_Entity);
        }
    }
}
