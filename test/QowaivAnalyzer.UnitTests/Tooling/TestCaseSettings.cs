using Microsoft.CodeAnalysis;
using System;
using System.IO;
using CS = Microsoft.CodeAnalysis.CSharp;
using VB = Microsoft.CodeAnalysis.VisualBasic;

namespace QowaivAnalyzer.UnitTests.Tooling
{
    public class TestCaseSettings
    {
        public static TestCaseSettings CSharp6
        {
            get
            {
                return new TestCaseSettings()
                {
                    Language = LanguageNames.CSharp,
                    ParseOptions = new CS.CSharpParseOptions(CS.LanguageVersion.CSharp6),
                    CompilationOptions = new CS.CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
                };
            }
        }

        public static TestCaseSettings CSharp7
        {
            get
            {
                return new TestCaseSettings()
                {
                    Language = LanguageNames.CSharp,
                    ParseOptions = new CS.CSharpParseOptions(CS.LanguageVersion.CSharp6),
                    CompilationOptions = new CS.CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
                };
            }
        }

        public static TestCaseSettings VisualBasic14
        {
            get
            {
                return new TestCaseSettings()
                {
                    Language = LanguageNames.VisualBasic,
                    ParseOptions = new VB.VisualBasicParseOptions(VB.LanguageVersion.VisualBasic14),
                    CompilationOptions = new VB.VisualBasicCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
                };
            }
        }

        public string Language { get; set; }
        public ParseOptions ParseOptions { get; set; }
        public CompilationOptions CompilationOptions { get; set; }

        public static TestCaseSettings FromFile(FileInfo codeFile)
        {
            if (codeFile.Name.EndsWith(".cs"))
            {
                return CSharp6;
            }
            if (codeFile.Name.EndsWith(".vb"))
            {
                return VisualBasic14;
            }
            throw new NotSupportedException();
        }
    }
}
