using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Recurrency;

namespace RecurrencyTests
{
    [TestFixture]
    public class YearlyRecurrencyTests
    {
        [Test]
        public void Create_FromPattern_DayOfMonth()
        {
            YearlyRecurrency yearly = new YearlyRecurrency("Y 20110201 20150201 0000 0001 M 14 0 03");
            Assert.AreEqual(new DateTime(2011, 2, 01), yearly.StartDate);
            Assert.AreEqual(new DateTime(2015, 2, 01), yearly.EndDate);
            Assert.AreEqual(0, yearly.Occurrences);
            Assert.AreEqual(1, yearly.Interval);
            Assert.AreEqual(MonthlyType.MonthDay, yearly.Type);
            Assert.AreEqual(14, yearly.Day);
            Assert.AreEqual(3, yearly.Month);
        }

        [Test]
        public void Create_FromPattern_DayOfWeek()
        {
            YearlyRecurrency yearly = new YearlyRecurrency("Y 20110201 00000000 0006 0002 W 00 2 04");
            Assert.AreEqual(new DateTime(2011, 2, 01), yearly.StartDate);
            Assert.IsNull(yearly.EndDate);
            Assert.AreEqual(6, yearly.Occurrences);
            Assert.AreEqual(2, yearly.Interval);
            Assert.AreEqual(MonthlyType.Weekday, yearly.Type);
            Assert.AreEqual(00, yearly.Day);
            Assert.AreEqual(DayOfWeek.Monday, yearly.DayOfWeek);
            Assert.AreEqual(2, yearly.Index);
            Assert.AreEqual(DayIndex.Third, yearly.DayIndex);
            Assert.AreEqual(4, yearly.Month);
        }

        [Test]
        public void Create_DayOfMonth()
        {
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2011, 2, 01), new DateTime(2011, 11, 30), 1, 14, 3);
            Assert.AreEqual(new DateTime(2011, 2, 01), yearly.StartDate);
            Assert.AreEqual(new DateTime(2011, 11, 30), yearly.EndDate);
            Assert.AreEqual(0, yearly.Occurrences);
            Assert.AreEqual(1, yearly.Interval);
            Assert.AreEqual(MonthlyType.MonthDay, yearly.Type);
            Assert.AreEqual(14, yearly.Day);
            Assert.AreEqual(3, yearly.Month);
        }

        [Test]
        public void Create_DayOfWeek()
        {
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2011, 2, 01), 6, 2, DayOfWeek.Tuesday, DayIndex.Fourth, 3);
            Assert.AreEqual(new DateTime(2011, 2, 01), yearly.StartDate);
            Assert.IsNull(yearly.EndDate);
            Assert.AreEqual(6, yearly.Occurrences);
            Assert.AreEqual(2, yearly.Interval);
            Assert.AreEqual(MonthlyType.Weekday, yearly.Type);
            Assert.AreEqual(DayOfWeek.Tuesday, yearly.DayOfWeek);
            Assert.AreEqual(01, yearly.Day);

            Assert.AreEqual(3, yearly.Index);
            Assert.AreEqual(DayIndex.Fourth, yearly.DayIndex);
            Assert.AreEqual(3, yearly.Month);
        }

        [Test]
        public void GetDayInYear_DayOfMonth()
        {
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2011, 2, 01), new DateTime(2011, 11, 30), 1, 14, 3);
            Assert.AreEqual(MonthlyType.MonthDay, yearly.Type);
            Assert.AreEqual(14, yearly.Day);
            Assert.AreEqual(3, yearly.Month);

            Assert.AreEqual(new DateTime(2000, 3, 14), yearly.GetDayInYear(2000));
            Assert.AreEqual(new DateTime(2011, 3, 14), yearly.GetDayInYear(2011));
            Assert.AreEqual(new DateTime(2050, 3, 14), yearly.GetDayInYear(2050)); 
        }

        [Test]
        public void GetDayInYear_DayOfWeek()
        {
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2011, 2, 01), 6, 2, DayOfWeek.Tuesday, DayIndex.Second, 3);
            Assert.AreEqual(MonthlyType.Weekday, yearly.Type);
            Assert.AreEqual(DayOfWeek.Tuesday, yearly.DayOfWeek);
            Assert.AreEqual(DayIndex.Second, yearly.DayIndex);
            Assert.AreEqual(3, yearly.Month);

            // 2nd Tuesday in March
            Assert.AreEqual(new DateTime(2000, 3, 14), yearly.GetDayInYear(2000));
            Assert.AreEqual(new DateTime(2011, 3, 8), yearly.GetDayInYear(2011));
            Assert.AreEqual(new DateTime(2020, 3, 10), yearly.GetDayInYear(2020)); 

        }

        [Test]
        public void GetNextDate_DayOfMonth_Exact()
        {
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2012, 1, 01), new DateTime(2015, 11, 30), 1, 15, 3);
            Assert.AreEqual(new DateTime(2012, 1, 1), yearly.StartDate);
            Assert.AreEqual(new DateTime(2012, 3, 15), yearly.GetFirstDate());
            Assert.AreEqual(new DateTime(2013, 3, 15), yearly.GetNextDate(new DateTime(2012, 3, 15)));
            Assert.AreEqual(new DateTime(2014, 3, 15), yearly.GetNextDate(new DateTime(2013, 3, 15)));
            Assert.AreEqual(new DateTime(2015, 3, 15), yearly.GetNextDate(new DateTime(2014, 3, 15)));
            Assert.IsNull(yearly.GetNextDate(new DateTime(2015, 3, 15)));
        }

        [Test]
        public void GetNextDate_DayOfMonth_Inexact()
        {
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2012, 1, 01), new DateTime(2015, 11, 30), 1, 15, 3);
            Assert.AreEqual(new DateTime(2012, 1, 1), yearly.StartDate);
            Assert.AreEqual(new DateTime(2012, 3, 15), yearly.GetFirstDate());
            Assert.AreEqual(new DateTime(2013, 3, 15), yearly.GetNextDate(new DateTime(2012, 3, 15)));
            Assert.AreEqual(new DateTime(2013, 3, 15), yearly.GetNextDate(new DateTime(2012, 3, 16)));
            Assert.AreEqual(new DateTime(2013, 3, 15), yearly.GetNextDate(new DateTime(2012, 9, 15)));
            Assert.AreEqual(new DateTime(2013, 3, 15), yearly.GetNextDate(new DateTime(2012, 12,31)));
            Assert.AreEqual(new DateTime(2013, 3, 15), yearly.GetNextDate(new DateTime(2013, 3, 14)));

        }

        [Test]
        public void GetNextDate_DayOfWeek_Exact()
        {
            // 2nd Tuesday in March
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2011, 2, 01), 4, 2, DayOfWeek.Tuesday, DayIndex.Second, 3);
            Assert.AreEqual(new DateTime(2011, 2, 1), yearly.StartDate);
            Assert.AreEqual(new DateTime(2011, 3, 8), yearly.GetFirstDate());
            Assert.AreEqual(new DateTime(2013, 3, 12), yearly.GetNextDate(new DateTime(2011, 3, 8)));
            Assert.AreEqual(new DateTime(2015, 3, 10), yearly.GetNextDate(new DateTime(2013, 3, 12)));
            Assert.AreEqual(new DateTime(2017, 3, 14), yearly.GetNextDate(new DateTime(2015, 3, 10)));
            Assert.IsNull(yearly.GetNextDate(new DateTime(2017, 3, 14)));
        }

        [Test]
        public void GetNextDate_DayOfWeek_Inexact()
        {
            // 2nd Tuesday in March
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2011, 2, 01), 4, 2, DayOfWeek.Tuesday, DayIndex.Second, 3);

            Assert.AreEqual(new DateTime(2011, 3, 8), yearly.GetFirstDate());
            Assert.AreEqual(new DateTime(2011, 3, 8), yearly.GetNextDate(new DateTime(2010, 1, 1)));
            Assert.AreEqual(new DateTime(2011, 3, 8), yearly.GetNextDate(new DateTime(2010, 12, 31)));
            Assert.AreEqual(new DateTime(2011, 3, 8), yearly.GetNextDate(new DateTime(2011, 1, 1)));
            Assert.AreEqual(new DateTime(2011, 3, 8), yearly.GetNextDate(new DateTime(2011, 3, 7)));

            Assert.AreEqual(new DateTime(2013, 3, 12), yearly.GetNextDate(new DateTime(2011, 3, 8)));
            Assert.AreEqual(new DateTime(2013, 3, 12), yearly.GetNextDate(new DateTime(2011, 3, 9)));
            Assert.AreEqual(new DateTime(2013, 3, 12), yearly.GetNextDate(new DateTime(2011, 12, 31)));
            Assert.AreEqual(new DateTime(2013, 3, 12), yearly.GetNextDate(new DateTime(2012, 1, 1)));
            Assert.AreEqual(new DateTime(2013, 3, 12), yearly.GetNextDate(new DateTime(2012, 3, 8)));
            Assert.AreEqual(new DateTime(2013, 3, 12), yearly.GetNextDate(new DateTime(2012, 12, 31)));
            Assert.AreEqual(new DateTime(2013, 3, 12), yearly.GetNextDate(new DateTime(2013, 1, 1)));
            Assert.AreEqual(new DateTime(2013, 3, 12), yearly.GetNextDate(new DateTime(2013, 3, 11)));
        }

        [Test]
        public void GetFirstDate_DayOfMonth()
        {
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2011, 2, 01), new DateTime(2011, 11, 30), 1, 14, 3);
            Assert.AreEqual(new DateTime(2011, 3, 14), yearly.GetFirstDate());
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

            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2011, 1, 01), new DateTime(2011, 11, 30), 1, DayOfWeek.Tuesday, DayIndex.Third, 4);
            Assert.AreEqual(new DateTime(2011, 4, 19), yearly.GetFirstDate());
        }

        [Test]
        public void GetFirstDate_RollsIntoNextYear()
        {
            YearlyRecurrency yearly = new YearlyRecurrency(new DateTime(2011, 4, 20), new DateTime(2011, 11, 30), 1, 14, 4);
            Assert.AreEqual(new DateTime(2012, 4, 14), yearly.GetFirstDate()); // (2011, 4, 14) is before start date
        }

        [Test]
        public void GetPattern_DayOfMonth()
        {
            YearlyRecurrency yearly = new YearlyRecurrency("Y 20110201 20150201 0000 0001 M 14 0 03");
            Assert.AreEqual("Y201102012015020100000001M14003", yearly.GetPattern());
        }

        [Test]
        public void GetPattern_DayOfWeek()
        {
            YearlyRecurrency yearly = new YearlyRecurrency("Y 20110201 00000000 0006 0002 W 00 2 04");
            Assert.AreEqual("Y201102010000000000060002W00204", yearly.GetPattern());
        }

        [Test]
        public void GetTypeCode()
        {
            YearlyRecurrency yearly = new YearlyRecurrency("Y 20110201 00000000 0006 0002 W 00 2 04");
            Assert.AreEqual('Y', yearly.GetTypeCode());
        }
    }
}
