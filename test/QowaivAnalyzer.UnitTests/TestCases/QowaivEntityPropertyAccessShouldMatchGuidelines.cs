using System.Runtime.CompilerServices;

namespace Qowaiv.DomainModel
{
    public class Entity<TEntity> where TEntity : Entity<TEntity>
    {
        protected void SetProperty<T>(T value, [CallerMemberName] string propertyName = null) { /* dummy */ }
        protected T GetProperty<T>([CallerMemberName] string propertyName = null) => default(T);
    }
}

namespace QowaivAnalyzer.UnitTests.TestCases
{
    public class NoEntity
    {
        public int Compliant { get; set; }
    }

    public class QowaivEntityPropertyAccessShouldMatchGuidelines : Qowaiv.DomainModel.Entity<QowaivEntityPropertyAccessShouldMatchGuidelines>
    {
        public int CalculatedShouldBeIgnored => CompliantProp * 10;

        public int CompliantProp
        {
            get => GetProperty<int>();
            internal set => SetProperty(value);
        }

        public int CompliantWithWrongAccess
        {
            get => GetProperty<int>();
            internal set => SetProperty(value);
        }

        public int NotCompliantWithAuto { get; internal set; }

        public int NotCompliantWithUnderlying
        {
            get => CompliantProp;
            internal set => CompliantProp = value;
        }
    }
}


