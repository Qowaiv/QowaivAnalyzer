using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QowaivAnalyzer
{
    public static class TypeExtensions
    {
        public static bool DerivesFrom(this ITypeSymbol typeSymbol, KnownType type)
        {
            return typeSymbol.GetDerived().Any(tp => tp.Is(type));
        }

        public static IEnumerable<ITypeSymbol> GetDerived(this ITypeSymbol typeSymbol)
        {
            var currentType = typeSymbol;
            while (currentType != null)
            {
                yield return currentType;
                currentType = currentType.BaseType?.ConstructedFrom;
            }
        }

        public static bool Implements(this ITypeSymbol typeSymbol, KnownType type)
        {
            return typeSymbol != null && typeSymbol.AllInterfaces.Any(symbol => symbol.ConstructedFrom.Is(type));
        }

        private static bool IsMatch(ITypeSymbol typeSymbol, KnownType type)
        {
            return type.Matches(typeSymbol.SpecialType) || type.Matches(typeSymbol.ToDisplayString());
        }

        public static bool Is(this ITypeSymbol typeSymbol, KnownType type)
        {
            return typeSymbol != null && IsMatch(typeSymbol, type);
        }
    }
}
