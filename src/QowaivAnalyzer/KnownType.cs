using Microsoft.CodeAnalysis;
using System;

namespace QowaivAnalyzer
{
    /// <summary>Represents a (known) type.</summary>
    public partial struct KnownType
    {
        private readonly SpecialType specialType;
        private readonly bool isSpecialType;

        /// <summary>Creates a new instance of a <see cref="KnownType"/>.</summary>
        private KnownType(string name) : this(name, SpecialType.None, false) { }

        /// <summary>Creates a new instance of a <see cref="KnownType"/>.</summary>
        private KnownType(string name, SpecialType specialType, bool isSpecialType)
        {
            Name = name;
            this.specialType = specialType;
            this.isSpecialType = isSpecialType;
        }

        /// <summary>Gets the name of the type.</summary>
        public string Name { get; }

        /// <summary>Represents the type as a <see cref="string"/>.</summary>
        public override string ToString() => Name;

        /// <summary>Return true if there is a match, otherwise false.</summary>
        public bool Matches(string type) => !isSpecialType && Name == type;

        /// <summary>Return true if there is a match, otherwise false.</summary>
        public bool Matches(SpecialType type) => isSpecialType && specialType == type;

        /// <summary>Return true if there is a match, otherwise false.</summary>
        public bool Matches(ITypeSymbol type)
        {
            return
                type != null &&
                $"{type.ContainingNamespace}.{type.Name}" == Name;
        }

        /// <summary>Casts a <see cref="Type"/> to a <see cref="KnownType"/>.</summary>
        public static implicit operator KnownType(Type type) => new KnownType(type.FullName);

        /// <summary>Generates a <see cref="KnownType"/> from a <see cref="SpecialType"/>.</summary>
        public static KnownType FromType(Type type) => new KnownType(type.FullName);

        /// <summary>Generates a <see cref="KnownType"/> from a <see cref="SpecialType"/>.</summary>
        public static KnownType FromSpecialType(SpecialType specialType, string name)
        {
            return new KnownType(name, specialType, true);
        }
    }
}
