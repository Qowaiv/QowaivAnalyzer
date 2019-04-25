using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using NUnit.Framework;
using QowaivAnalyzer.UnitTests.Tooling;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace QowaivAnalyzer.UnitTests
{
    public static class AnalyzerAssert
    {
        //[DebuggerStepThrough]
        public static void Verify(
            DiagnosticResults exp, 
            string testCase,
            DiagnosticAnalyzer analyzer, 
            params MetadataReference[] additionalReferences)
        {
            var file = new FileInfo(Path.Combine(@"test\QowaivAnalyzer.UnitTests", testCase));

            var document = TestCase.Load(file, additionalReferences);
            var settings = TestCaseSettings.FromFile(file);

            var compilation = document.Project
                .WithParseOptions(settings.ParseOptions)
                .GetCompilationAsync().Result;

            var act = DiagnosticResults.FromCompilation(compilation, analyzer, settings);

            var sb = new StringBuilder();

            sb.AppendLine("Expected:");
            foreach (var e in exp)
            {
                sb.Append(e);
                sb.AppendLine(act.Contains(e) ? "" : " [missing]");
            }
            sb.AppendLine("Actual:");
            foreach (var a in act)
            {
                sb.Append(a);
                sb.AppendLine(exp.Contains(a) ? "" : " [extra]");
            }

            if (exp.Count == act.Count)
            {
                var ok = true;
                for (var i = 0; i < exp.Count; i++)
                {
                    ok &= act[i].Equals(exp[i]);
                }
                if (ok)
                {
                    return;
                }
            }
            
            Assert.Fail(sb.ToString());
        }
    }
}
