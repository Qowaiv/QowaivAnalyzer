using System;

namespace QowaivAnalyzer.UnitTests.TestCases
{
    public class UseInjectableDateTimeProvider
    {
        public void Test()
        {
            var act = DateTime.UtcNow;
            var exp = new DateTime(2017, 06, 11);
            var other = Now;
            var today = DateTime.Today;
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
