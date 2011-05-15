using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Recurrency;

namespace RecurrencyTests
{
    [TestFixture]
    public class DailyTests
    {
        [Test]
        public void GetNextDate_Xis1()
        {
            DailyRecurrency daily = new DailyRecurrency(new DateTime(2011, 4, 1), 5, DailyType.EveryXDays, 1);

            Assert.AreEqual(new DateTime(2011, 4, 1), daily.StartDate);
            Assert.AreEqual(new DateTime(2011, 4, 1), daily.GetFirstDate());
            Assert.AreEqual(new DateTime(2011, 4, 2), daily.GetNextDate(new DateTime(2011, 4, 1)));
            Assert.AreEqual(new DateTime(2011, 4, 3), daily.GetNextDate(new DateTime(2011, 4, 2)));
            Assert.AreEqual(new DateTime(2011, 4, 4), daily.GetNextDate(new DateTime(2011, 4, 3)));
            Assert.AreEqual(new DateTime(2011, 4, 5), daily.GetNextDate(new DateTime(2011, 4, 4)));
            Assert.AreEqual(null, daily.GetNextDate(new DateTime(2011, 4, 5)));
            
        }

        [Test]
        public void GetNextDate_Xis5_Exact()
        {
            DailyRecurrency daily = new DailyRecurrency(new DateTime(2011, 4, 5), new DateTime(2011, 4, 25), DailyType.EveryXDays, 5);

            Assert.AreEqual(new DateTime(2011, 4, 5), daily.StartDate);
            Assert.AreEqual(new DateTime(2011, 4, 5), daily.GetFirstDate());
            Assert.AreEqual(new DateTime(2011, 4, 10), daily.GetNextDate(new DateTime(2011, 4, 5)));
            Assert.AreEqual(new DateTime(2011, 4, 15), daily.GetNextDate(new DateTime(2011, 4, 10)));
            Assert.AreEqual(new DateTime(2011, 4, 20), daily.GetNextDate(new DateTime(2011, 4, 15)));
            Assert.AreEqual(new DateTime(2011, 4, 25), daily.GetNextDate(new DateTime(2011, 4, 20)));
            Assert.AreEqual(null, daily.GetNextDate(new DateTime(2011, 4, 25)));
        }

        [Test]
        public void GetNextDate_Xis5_Inexact()
        {
            DailyRecurrency daily = new DailyRecurrency(new DateTime(2011, 4, 5), new DateTime(2011, 4, 15), DailyType.EveryXDays, 5);

            Assert.AreEqual(new DateTime(2011, 4, 05), daily.GetNextDate(new DateTime(2011, 4, 3)));
            Assert.AreEqual(new DateTime(2011, 4, 05), daily.GetNextDate(new DateTime(2011, 4, 4)));

            Assert.AreEqual(new DateTime(2011, 4, 10), daily.GetNextDate(new DateTime(2011, 4, 5)));
            Assert.AreEqual(new DateTime(2011, 4, 10), daily.GetNextDate(new DateTime(2011, 4, 9)));

            Assert.AreEqual(new DateTime(2011, 4, 15), daily.GetNextDate(new DateTime(2011, 4, 10)));
            Assert.AreEqual(new DateTime(2011, 4, 15), daily.GetNextDate(new DateTime(2011, 4, 14)));

            Assert.AreEqual(null, daily.GetNextDate(new DateTime(2011, 4, 15)));
            Assert.AreEqual(null, daily.GetNextDate(new DateTime(2011, 4, 16)));

        }

        [Test]
        public void GetNextDate_Weeky()
        {
            DailyRecurrency daily = new DailyRecurrency(new DateTime(2011, 4, 1), 5, DailyType.Weekdays);

            Assert.AreEqual(new DateTime(2011, 4, 1), daily.StartDate);        
            Assert.AreEqual(new DateTime(2011, 4, 1), daily.GetFirstDate());    //fri 
            Assert.AreEqual(new DateTime(2011, 4, 4), daily.GetNextDate(new DateTime(2011, 4, 1)));  // fri -> mon
            Assert.AreEqual(new DateTime(2011, 4, 4), daily.GetNextDate(new DateTime(2011, 4, 2)));  // sat -> mon
            Assert.AreEqual(new DateTime(2011, 4, 4), daily.GetNextDate(new DateTime(2011, 4, 3)));  // sun -> mon
            Assert.AreEqual(new DateTime(2011, 4, 5), daily.GetNextDate(new DateTime(2011, 4, 4)));  // mon -> tues
            Assert.AreEqual(new DateTime(2011, 4, 6), daily.GetNextDate(new DateTime(2011, 4, 5)));  // mon -> tues
            Assert.AreEqual(new DateTime(2011, 4, 7), daily.GetNextDate(new DateTime(2011, 4, 6)));  // mon -> tues
            Assert.AreEqual(null, daily.GetNextDate(new DateTime(2011, 4, 7)));
        }

        [Test]
        public void SetTypeAndDaysApart()
        {
            DailyRecurrency daily = new DailyRecurrency(new DateTime(2011, 4, 1), 5, DailyType.EveryXDays, 10);
            Assert.AreEqual(DailyType.EveryXDays, daily.Type);
            Assert.AreEqual(10, daily.Interval);
        }

        [Test]
        public void GetFirstDate_WeekDays_IsWeekDay()
        {
            DailyRecurrency daily = new DailyRecurrency(new DateTime(2011, 5, 9), 5, DailyType.Weekdays); // Mon
            Assert.AreEqual(new DateTime(2011, 5, 9), daily.GetFirstDate());

            daily = new DailyRecurrency(new DateTime(2011, 5, 10), 5, DailyType.Weekdays); // Tues
            Assert.AreEqual(new DateTime(2011, 5, 10), daily.GetFirstDate());

            daily = new DailyRecurrency(new DateTime(2011, 5, 11), 5, DailyType.Weekdays); // Wed
            Assert.AreEqual(new DateTime(2011, 5, 11), daily.GetFirstDate());

            daily = new DailyRecurrency(new DateTime(2011, 5, 12), 5, DailyType.Weekdays); // Thurs
            Assert.AreEqual(new DateTime(2011, 5, 12), daily.GetFirstDate());

            daily = new DailyRecurrency(new DateTime(2011, 5, 13), 5, DailyType.Weekdays); // Fri
            Assert.AreEqual(new DateTime(2011, 5, 13), daily.GetFirstDate());
        }

        [Test]
        public void GetFirstDate_WeekDays_IsWeekEnd()
        {
            DailyRecurrency daily = new DailyRecurrency(new DateTime(2011, 5, 14), 5, DailyType.Weekdays); // Mon
            Assert.AreEqual(new DateTime(2011, 5, 16), daily.GetFirstDate());

            daily = new DailyRecurrency(new DateTime(2011, 5, 15), 5, DailyType.Weekdays); // Tues
            Assert.AreEqual(new DateTime(2011, 5, 16), daily.GetFirstDate());

        }

        [Test]
        public void Create_FromPattern_X()
        {
            DailyRecurrency daily = new DailyRecurrency("D2011051200000000000156000050X");
            Assert.AreEqual(new DateTime(2011, 5, 12), daily.StartDate);
            Assert.IsNull(daily.EndDate);
            Assert.AreEqual(156, daily.Occurrences);
            Assert.AreEqual(DailyType.EveryXDays, daily.Type);
            Assert.AreEqual(50, daily.Interval);
        }

        [Test]
        public void Create_FromPattern_Weekly()
        {
            DailyRecurrency daily = new DailyRecurrency("D 20110512 00000000 000156 000001 W");
            Assert.AreEqual(new DateTime(2011, 5, 12), daily.StartDate);
            Assert.IsNull(daily.EndDate);
            Assert.AreEqual(156, daily.Occurrences);
            Assert.AreEqual(DailyType.Weekdays, daily.Type);
        }

        [Test]
        public void GetPattern_X()
        {
            DailyRecurrency daily = new DailyRecurrency(new DateTime(2011, 5, 12), new DateTime(2011, 05, 22), DailyType.EveryXDays, 156);
            Assert.AreEqual("D2011051220110522000000000156X", daily.GetPattern());
        }

        [Test]
        public void GetPattern_Weekly()
        {
            DailyRecurrency daily = new DailyRecurrency(new DateTime(2011, 5, 12), 10, DailyType.Weekdays);
            Assert.AreEqual("D2011051200000000000010000001W", daily.GetPattern());
        }
    }
}
