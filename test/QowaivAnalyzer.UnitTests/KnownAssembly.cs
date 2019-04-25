using Microsoft.CodeAnalysis;

namespace QowaivAnalyzer.UnitTests
{
    public static class KnownAssembly
    {
        public static MetadataReference Microsoft_VisualBasic = FromType<Microsoft.VisualBasic.DateAndTime>();
        public static MetadataReference FromType<T>() => MetadataReference.CreateFromFile(typeof(T).Assembly.Location);
    }
}
