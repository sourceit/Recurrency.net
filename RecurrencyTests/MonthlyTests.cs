using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Recurrency;

namespace RecurrencyTests
{
    [TestFixture]
    public class MonthlyTests
    {
        [Test]
        public void Create_FromPattern_DayOfMonth()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency("M 20110201 20111130 0000 0001 M 14 0");
            Assert.AreEqual(new DateTime(2011, 2, 01), monthly.StartDate);
            Assert.AreEqual(new DateTime(2011, 11, 30), monthly.EndDate);
            Assert.AreEqual(0, monthly.Occurrences);
            Assert.AreEqual(1, monthly.Interval);
            Assert.AreEqual(MonthlyType.MonthDay, monthly.Type);
            Assert.AreEqual(14, monthly.Day);
        }

        [Test]
        public void Create_FromPattern_DayOfWeek()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency("M 20110201 00000000 0006 0002 W 01 3");
            Assert.AreEqual(new DateTime(2011, 2, 01), monthly.StartDate);
            Assert.IsNull(monthly.EndDate);
            Assert.AreEqual(6, monthly.Occurrences);
            Assert.AreEqual(2, monthly.Interval);
            Assert.AreEqual(MonthlyType.Weekday, monthly.Type);
            Assert.AreEqual(01, monthly.Day);
            Assert.AreEqual(DayOfWeek.Tuesday, monthly.DayOfWeek);
            Assert.AreEqual(3, monthly.Index);
        }

        [Test]
        public void Create_DayOfMonth()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 2, 01), new DateTime(2011, 11, 30), 1, 14);
            Assert.AreEqual(new DateTime(2011, 2, 01), monthly.StartDate);
            Assert.AreEqual(new DateTime(2011, 11, 30), monthly.EndDate);
            Assert.AreEqual(0, monthly.Occurrences);
            Assert.AreEqual(1, monthly.Interval);
            Assert.AreEqual(MonthlyType.MonthDay, monthly.Type);
            Assert.AreEqual(14, monthly.Day);
        }

        [Test]
        public void Create_DayOfWeek()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 2, 01), 6, 2, DayOfWeek.Tuesday, DayIndex.Fourth);
            Assert.AreEqual(new DateTime(2011, 2, 01), monthly.StartDate);
            Assert.IsNull(monthly.EndDate);
            Assert.AreEqual(6, monthly.Occurrences);
            Assert.AreEqual(2, monthly.Interval);
            Assert.AreEqual(MonthlyType.Weekday, monthly.Type);
            Assert.AreEqual(DayOfWeek.Tuesday, monthly.DayOfWeek);
            Assert.AreEqual(01, monthly.Day);
            
            Assert.AreEqual(3, monthly.Index);
            Assert.AreEqual(DayIndex.Fourth, monthly.DayIndex);
        }

        [Test]
        public void GetFirstDate_DayOfMonth()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 2, 01), new DateTime(2011, 11, 30), 1, 14);
            Assert.AreEqual(new DateTime(2011, 2, 14), monthly.GetFirstDate());
        }

        [Test]
        public void GetFirstDate_DayOfWeek()
        {
            // April 2011          
            // M  T  W  T  F  S  S   
            //             1  2  3  
            // 4  5  6  7  8  9  10 
            // 11 12 13 14 15 16 17 
            // 18 19 20 21 22 23 24 
            // 25 26 27 28 29 30

            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 4, 01), new DateTime(2011, 11, 30), 1, DayOfWeek.Tuesday, DayIndex.Third);
            Assert.AreEqual(new DateTime(2011, 4, 19), monthly.GetFirstDate());
        }

        [Test]
        public void GetFirstDate_DayOfMonth_RollsIntoNextMonth()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 2, 20), new DateTime(2011, 11, 30), 1, 14);
            Assert.AreEqual(new DateTime(2011, 3, 14), monthly.GetFirstDate()); // (2011, 2, 14) is before start date
        }

        [Test]
        public void GetFirstDate_DayOfWeek_RollsIntoNextMonth()
        {
            // April 2011          
            // M  T  W  T  F  S  S   
            //             1  2  3  
            // 4  5  6  7  8  9  10 
            // 11 12 13 14 15 16 17 
            // 18 19 20 21 22 23 24 
            // 25 26 27 28 29 30

            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 3, 20), new DateTime(2011, 11, 30), 1, DayOfWeek.Tuesday, DayIndex.Third);
            Assert.AreEqual(new DateTime(2011, 4, 19), monthly.GetFirstDate()); // (2011, 3, 15) is before start date
        }

        [Test]
        public void SetDayOfWeek()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 4, 01), new DateTime(2011, 11, 30), 1, DayOfWeek.Monday, DayIndex.Third);

            monthly.DayOfWeek = DayOfWeek.Monday;
            Assert.AreEqual(0, monthly.Day);
            monthly.DayOfWeek = DayOfWeek.Tuesday;
            Assert.AreEqual(1, monthly.Day);

            monthly.DayOfWeek = DayOfWeek.Saturday;
            Assert.AreEqual(5, monthly.Day);

            monthly.DayOfWeek = DayOfWeek.Sunday;
            Assert.AreEqual(6, monthly.Day);
        }

        [Test]
        public void GetDayOfWeek()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 4, 01), new DateTime(2011, 11, 30), 1, DayOfWeek.Monday, DayIndex.Third);
            monthly.Day = 0;
            Assert.AreEqual(DayOfWeek.Monday, monthly.DayOfWeek);

            monthly.Day = 1;
            Assert.AreEqual(DayOfWeek.Tuesday, monthly.DayOfWeek);

            monthly.Day = 5;
            Assert.AreEqual(DayOfWeek.Saturday, monthly.DayOfWeek);

            monthly.Day = 6;
            Assert.AreEqual(DayOfWeek.Sunday, monthly.DayOfWeek);
        }

        [Test]
        public void GetNextDate_DayOfMonth_Exact()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 1, 1), 4, 1, 15);
            Assert.AreEqual(new DateTime(2011, 1, 1), monthly.StartDate);
            Assert.AreEqual(new DateTime(2011, 1, 15), monthly.GetFirstDate());
            Assert.AreEqual(new DateTime(2011, 2, 15), monthly.GetNextDate(new DateTime(2011, 1, 15)));
            Assert.AreEqual(new DateTime(2011, 3, 15), monthly.GetNextDate(new DateTime(2011, 2, 15)));
            Assert.AreEqual(new DateTime(2011, 4, 15), monthly.GetNextDate(new DateTime(2011, 3, 15)));
            Assert.IsNull(monthly.GetNextDate(new DateTime(2011, 4, 15)));
        }

        [Test]
        public void GetNextDate_DayOfMonth_Inexact()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 1, 1), 4, 1, 15);
            Assert.AreEqual(new DateTime(2011, 1, 1), monthly.StartDate);
            Assert.AreEqual(new DateTime(2011, 1, 15), monthly.GetFirstDate());
            Assert.AreEqual(new DateTime(2011, 1, 15), monthly.GetNextDate(new DateTime(2011, 1, 1)));
            Assert.AreEqual(new DateTime(2011, 1, 15), monthly.GetNextDate(new DateTime(2011, 1, 14)));

            Assert.AreEqual(new DateTime(2011, 2, 15), monthly.GetNextDate(new DateTime(2011, 1, 15)));
            Assert.AreEqual(new DateTime(2011, 2, 15), monthly.GetNextDate(new DateTime(2011, 1, 16)));
            Assert.AreEqual(new DateTime(2011, 2, 15), monthly.GetNextDate(new DateTime(2011, 2, 14)));

            Assert.AreEqual(new DateTime(2011, 3, 15), monthly.GetNextDate(new DateTime(2011, 2, 15)));
            Assert.AreEqual(new DateTime(2011, 3, 15), monthly.GetNextDate(new DateTime(2011, 2, 16)));
            Assert.AreEqual(new DateTime(2011, 3, 15), monthly.GetNextDate(new DateTime(2011, 3, 14)));

            Assert.AreEqual(new DateTime(2011, 4, 15), monthly.GetNextDate(new DateTime(2011, 3, 15)));
            Assert.AreEqual(new DateTime(2011, 4, 15), monthly.GetNextDate(new DateTime(2011, 3, 16)));
            Assert.AreEqual(new DateTime(2011, 4, 15), monthly.GetNextDate(new DateTime(2011, 4, 14)));

            Assert.IsNull(monthly.GetNextDate(new DateTime(2011, 4, 15)));
        }

        [Test]
        public void GetNextDate_DayOfWeek_Exact()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 4, 01), new DateTime(2011, 11, 30), 3, DayOfWeek.Monday, DayIndex.Third);
            Assert.AreEqual(new DateTime(2011, 4, 1), monthly.StartDate);
            Assert.AreEqual(new DateTime(2011, 4, 18), monthly.GetFirstDate());
            Assert.AreEqual(new DateTime(2011, 7, 18), monthly.GetNextDate(new DateTime(2011, 4, 18)));
            Assert.AreEqual(new DateTime(2011, 10, 17), monthly.GetNextDate(new DateTime(2011, 7, 18)));
            Assert.IsNull(monthly.GetNextDate(new DateTime(2011, 10, 17)));
        }

        [Test]
        public void GetNextDate_DayOfWeek_Inexact()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency(new DateTime(2011, 4, 01), new DateTime(2011, 11, 30), 3, DayOfWeek.Monday, DayIndex.Third);
            Assert.AreEqual(new DateTime(2011, 4, 1), monthly.StartDate);
            Assert.AreEqual(new DateTime(2011, 4, 18), monthly.GetFirstDate());
            Assert.AreEqual(new DateTime(2011, 4, 18), monthly.GetNextDate(new DateTime(2011, 1, 1)));
            Assert.AreEqual(new DateTime(2011, 4, 18), monthly.GetNextDate(new DateTime(2011, 4, 17)));

            Assert.AreEqual(new DateTime(2011, 7, 18), monthly.GetNextDate(new DateTime(2011, 4, 18)));
            Assert.AreEqual(new DateTime(2011, 7, 18), monthly.GetNextDate(new DateTime(2011, 4, 19)));
            Assert.AreEqual(new DateTime(2011, 7, 18), monthly.GetNextDate(new DateTime(2011, 5, 1)));
            Assert.AreEqual(new DateTime(2011, 7, 18), monthly.GetNextDate(new DateTime(2011, 6, 1)));
            Assert.AreEqual(new DateTime(2011, 7, 18), monthly.GetNextDate(new DateTime(2011, 7, 17)));
        }

        [Test]
        public void GetPattern_DayOfMonth()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency("M 20110201 20111130 0000 0001 M 14 0");
            Assert.AreEqual("M201102012011113000000001M140", monthly.GetPattern());
        }

        [Test]
        public void GetPattern_DayOfWeek()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency("M 20110201 00000000 0006 0002 W 01 3");
            Assert.AreEqual("M201102010000000000060002W013", monthly.GetPattern());
        }

        [Test]
        public void GetTypeCode()
        {
            MonthlyRecurrency monthly = new MonthlyRecurrency("M 20110201 00000000 0006 0002 W 01 3");
            Assert.AreEqual('M', monthly.GetTypeCode());
        }
    }
}
