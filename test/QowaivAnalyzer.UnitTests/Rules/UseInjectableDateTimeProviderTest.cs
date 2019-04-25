using NUnit.Framework;

namespace QowaivAnalyzer.UnitTests.Rules
{
    public class UseInjectableDateTimeProviderTest
    {
        [Test]
        public void UseInjectableDateTimeProvider_CSharp()
        {
            var exp = new DiagnosticResults()
                .Add(09, 23, "DateTime.UtcNow")
                .Add(12, 25, "DateTime.Today")
                .Add(17, 26, "DateTime.Now");

            AnalyzerAssert.Verify(exp, @"TestCases\UseInjectableDateTimeProvider.cs",
                new QowaivAnalyzer.Rules.CSharp.UseInjectableDateTimeProvider());
        }

        [Test]
        public void UseInjectableDateTimeProvider_VisualBasic()
        {
            var exp = new DiagnosticResults()
                .Add(09, 31, "Date.UtcNow")
                .Add(12, 33, "Date.Today")
                .Add(13, 30, "= Now")
                .Add(18, 24, "DateTime.Now");

            AnalyzerAssert.Verify(exp, @"TestCases\UseInjectableDateTimeProvider.vb",
                new QowaivAnalyzer.Rules.VisualBasic.UseInjectableDateTimeProvider(), KnownAssembly.Microsoft_VisualBasic);
        }
    }
}
