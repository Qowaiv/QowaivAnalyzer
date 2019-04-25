using Microsoft.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace QowaivAnalyzer.UnitTests.Tooling
{
    public static class TestCase
    {
        public static readonly MetadataReference SystemAssembly = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
        public static readonly MetadataReference SystemLinqAssembly = MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location);
        public static Document Load(FileInfo file, params MetadataReference[] additionalReferences)
        {
            var settings = TestCaseSettings.FromFile(file);

            var text = File.ReadAllText(file.FullName, Encoding.UTF8);

            using (var workspace = new AdhocWorkspace())
            {
                var document = workspace.CurrentSolution
                    .AddProject("TestCase", "TestCase.dll", settings.Language)
                    .AddMetadataReference(SystemAssembly)
                    .AddMetadataReference(SystemLinqAssembly)
                    .AddMetadataReferences(additionalReferences)
                    .AddDocument(file.Name, text);

                return document;
            }
        }
    }
}
