using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using QowaivAnalyzer.UnitTests.Tooling;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

namespace QowaivAnalyzer.UnitTests
{
    public class DiagnosticResults : List<DiagnosticResult>
    {
        public DiagnosticResults Add(int line, int pos, string text)
        {
            Add(new DiagnosticResult(line, pos, text));
            return this;
        }

        public static DiagnosticResults FromCompilation(Compilation compilation, DiagnosticAnalyzer analyzer, TestCaseSettings settings)
        {
            var results = new DiagnosticResults();

            var analyzers = new[] { analyzer }.ToImmutableArray();

            using (var cancel = new CancellationTokenSource())
            {
                var diagnostics = analyzer.SupportedDiagnostics
                    .ToDictionary(d => d.Id, d => ReportDiagnostic.Warn);

                diagnostics["AD0001"] = ReportDiagnostic.Error;

                var compilationOptions = settings.CompilationOptions.WithSpecificDiagnosticOptions(diagnostics);

                var compilationWithAnalyzer = compilation
                    .WithOptions(compilationOptions)
                    .WithAnalyzers(analyzers, cancellationToken: cancel.Token);

                var id = analyzer.SupportedDiagnostics.Single().Id;

                foreach (var res in compilationWithAnalyzer
                    .GetAllDiagnosticsAsync().Result)
                {
                    if (res.Id == id)
                    {
                        results.Add(DiagnosticResult.FromLocation(res.Location));
                        Console.WriteLine(res);
                    }
                    else if (res.Severity == DiagnosticSeverity.Error)
                    {
                        throw new ApplicationException(res.ToString());
                    }
                }
                results.Sort();
                return results;
            }
        }
    }
}
