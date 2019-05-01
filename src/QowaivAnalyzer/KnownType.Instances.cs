using Microsoft.CodeAnalysis;
using System;

namespace QowaivAnalyzer
{
    public partial struct KnownType
    {
        /// <summary><see cref="bool"/> type.</summary>
        public static readonly KnownType System_Boolean = FromSpecialType(SpecialType.System_Boolean, "System.Boolean");
        
        /// <summary><see cref="DateTime"/> type.</summary>
        public static readonly KnownType System_DateTime = FromSpecialType(SpecialType.System_DateTime, "System.DateTime");
        
        /// <summary><see cref="int"/> type.</summary>
        public static readonly KnownType System_Int32 = FromSpecialType(SpecialType.System_Int32, "System.Int32");

        /// <summary><see cref="EventArgs"/> type.</summary>
        public static readonly KnownType System_EventArgs = typeof(EventArgs);

        /// <summary><c>Microsoft.VisualBasic.DateAndTime</c> type.</summary>
        public static readonly KnownType Microsoft_VisualBasic_DateAndTime = new KnownType("Microsoft.VisualBasic.DateAndTime");
    }
}
