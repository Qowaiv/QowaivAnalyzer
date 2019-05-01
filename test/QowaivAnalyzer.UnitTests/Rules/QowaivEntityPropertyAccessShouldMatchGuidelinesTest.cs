using NUnit.Framework;
using QowaivAnalyzer.Rules.CSharp;

namespace QowaivAnalyzer.UnitTests.Rules
{
    public class QowaivEntityPropertyAccessShouldMatchGuidelinesTest
    {
        [Test]
        public void QowaivEntityPropertyAccessShouldMatchGuidelines()
        {
            var exp = new DiagnosticResults()
                .Add(09, 23, "DateTime.UtcNow")
                .Add(12, 25, "DateTime.Today")
                .Add(17, 26, "DateTime.Now");

            AnalyzerAssert.Verify(exp, @"TestCases\QowaivEntityPropertyAccessShouldMatchGuidelines.cs",
                new QowaivEntityPropertyAccessShouldMatchGuidelines());
        }
    }
}
