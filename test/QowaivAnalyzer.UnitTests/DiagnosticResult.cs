using Microsoft.CodeAnalysis;
using System;

namespace QowaivAnalyzer.UnitTests
{
    public struct DiagnosticResult : IComparable<DiagnosticResult>
    {
        public DiagnosticResult(int line, int pos, string text)
        {
            Line = line;
            Pos = pos;
            Text = text;
        }

        public int Line { get; }
        public int Pos { get; }
        public string Text { get; }

        public override string ToString()
        {
            return string.Format("@{0}:{1} {2}", Line, Pos, Text);
        }
        public int CompareTo(DiagnosticResult other)
        {
            var compare = Line.CompareTo(other.Line);
            if (compare == 0)
            {
                return Pos.CompareTo(other.Pos);
            }
            return compare;
        }

        public static DiagnosticResult FromLocation(Location location)
        {
            var sp = location.GetLineSpan();
            var ln = location.SourceTree.GetText().Lines[sp.StartLinePosition.Line];
            var tx = ln.ToString().Substring(sp.StartLinePosition.Character, sp.Span.End.Character - sp.Span.Start.Character);

            return new DiagnosticResult
            (
                sp.StartLinePosition.Line + 1,
                sp.StartLinePosition.Character + 1,
                tx
            );
        }
    }
}
