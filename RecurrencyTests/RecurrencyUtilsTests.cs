using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Recurrency;

namespace RecurrencyTests
{
    [TestFixture]
    public class RecurrencyUtilsTests
    {
        [Test]
        public void Create_Daily_FromPattern()
        {
            var reccur = RecurrencyUtils.Create("D 20110512 00000000 0156 0050 X");
            DailyRecurrency daily = reccur as DailyRecurrency;
            Assert.IsNotNull(daily);
            Assert.AreEqual(new DateTime(2011, 5, 12), daily.StartDate);
            Assert.IsNull(daily.EndDate);
            Assert.AreEqual(156, daily.Occurrences);
            Assert.AreEqual(DailyType.EveryXDays, daily.Type);
            Assert.AreEqual(50, daily.Interval);
        }

        [Test]
        public void Create_Weekly_FromPattern()
        {
            var reccur = RecurrencyUtils.Create("W 20110512 00000000 0156 0050 YNYNYNN");
            WeeklyRecurrency weekly = reccur as WeeklyRecurrency;

            Assert.AreEqual(new DateTime(2011, 5, 12), weekly.StartDate);
            Assert.IsNull(weekly.EndDate);
            Assert.AreEqual(156, weekly.Occurrences);
            Assert.AreEqual(50, weekly.Interval);
            Assert.IsTrue(weekly.Monday);
            Assert.IsFalse(weekly.Tuesday);
            Assert.IsTrue(weekly.Wednesday);
            Assert.IsFalse(weekly.Thursday);
            Assert.IsTrue(weekly.Friday);
            Assert.IsFalse(weekly.Saturday);
            Assert.IsFalse(weekly.Sunday);
        }

        [Test]
        public void Create_Monthly_FromPattern()
        {
            var reccur = RecurrencyUtils.Create("M 20110201 00000000 0006 0002 W 01 3");
            MonthlyRecurrency monthly = reccur as MonthlyRecurrency;
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
        public void Create_Yearly_FromPattern()
        {
            var reccur = RecurrencyUtils.Create("Y 20110201 20150201 0000 0001 M 14 0 03");
            YearlyRecurrency yearly = reccur as YearlyRecurrency;
            Assert.AreEqual(new DateTime(2011, 2, 01), yearly.StartDate);
            Assert.AreEqual(new DateTime(2015, 2, 01), yearly.EndDate);
            Assert.AreEqual(0, yearly.Occurrences);
            Assert.AreEqual(1, yearly.Interval);
            Assert.AreEqual(MonthlyType.MonthDay, yearly.Type);
            Assert.AreEqual(14, yearly.Day);
            Assert.AreEqual(3, yearly.Month);
        } 
    }
}
