using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Recurrency;

namespace RecurrencyTests
{
    [TestFixture]
    public class RecurrencyInfoTests
    {
        private void CheckEquals_Base(RecurrencyInfo info, BaseRecurrency recurrency)
        {
            Assert.AreEqual(recurrency.StartDate, info.StartDate);
            Assert.AreEqual(recurrency.EndDate, info.EndDate);
            Assert.AreEqual(recurrency.Occurrences, info.Occurrences);
        }

        private void CheckEquals(RecurrencyInfo info, DailyRecurrency recurrency)
        {
            Assert.AreEqual(recurrency.Interval, info.DailyInterval);
            Assert.AreEqual(recurrency.Type, info.DailyType);
        }

        private void CheckEquals(RecurrencyInfo info, WeeklyRecurrency recurrency)
        {
            Assert.AreEqual(recurrency.Interval, info.WeeklyInterval);
            Assert.AreEqual(recurrency.Monday, info.Monday);
        }

        private void CheckEquals(RecurrencyInfo info, MonthlyRecurrency recurrency)
        {
            Assert.AreEqual(recurrency.Interval, info.MonthlyInterval);
            Assert.AreEqual(recurrency.Type, info.MonthlyType);
            Assert.AreEqual(recurrency.Day, info.MonthlyDay);
            Assert.AreEqual(recurrency.DayOfWeek, info.MonthlyDayOfWeek);
            Assert.AreEqual(recurrency.DayIndex, info.MonthlyDayIndex);
        }

        private void CheckEquals(RecurrencyInfo info, YearlyRecurrency recurrency)
        {
            Assert.AreEqual(recurrency.Interval, info.YearlyInterval);
            Assert.AreEqual(recurrency.Type, info.YearlyType);
            Assert.AreEqual(recurrency.Day, info.YearlyDay);
            Assert.AreEqual(recurrency.DayOfWeek, info.YearlyDayOfWeek);
            Assert.AreEqual(recurrency.DayIndex, info.YearlyDayIndex);
            Assert.AreEqual(recurrency.Month, info.YearlyMonth);
        }

        private void CheckEmpty_Base(RecurrencyInfo info)
        {
            CheckEquals(info, new DailyRecurrency(DateTime.Today));
        }
        private void CheckEmpty_Daily(RecurrencyInfo info)
        {
            CheckEquals(info, new DailyRecurrency(DateTime.Today));
        }

        private void CheckEmpty_Weekly(RecurrencyInfo info)
        {
            CheckEquals(info, new WeeklyRecurrency(DateTime.Today) {Days = WeeklyRecurrency.WeekDays });
        }

        private void CheckEmpty_Monthly(RecurrencyInfo info)
        {
            CheckEquals(info, new MonthlyRecurrency(DateTime.Today, dayOfMonth: 1));
        }

        private void CheckEmpty_Yearly(RecurrencyInfo info)
        {
            CheckEquals(info, new YearlyRecurrency(DateTime.Today, dayOfMonth: 1));
        }

        [Test]
        public void CheckDefaultValues()
        {
            var info = new RecurrencyInfo();

            CheckEmpty_Base(info);
            CheckEmpty_Daily(info);
            CheckEmpty_Weekly(info);
            CheckEmpty_Monthly(info);
            CheckEmpty_Yearly(info);

            Assert.AreEqual(RecurrencyType.Daily, info.Type);
        }

        [Test]
        public void Constructor_FromDaily()
        {
            var recurrency = new DailyRecurrency(new DateTime(2011, 10, 7), 5, DailyType.EveryXDays, 2);
            var info = new RecurrencyInfo(recurrency);

            CheckEquals_Base(info, recurrency);
            CheckEquals(info, recurrency);

            CheckEmpty_Weekly(info);
            CheckEmpty_Monthly(info);
            CheckEmpty_Yearly(info);

            Assert.AreEqual(RecurrencyType.Daily, info.Type);
        }

        [Test]
        public void Constructor_FromWeekly()
        {
            var recurrency = new WeeklyRecurrency(new DateTime(2011, 10, 7), new DateTime(2012, 4, 7), 2) { Days = WeeklyRecurrency.WeekEnd };
            var info = new RecurrencyInfo(recurrency);

            CheckEquals_Base(info, recurrency);
            CheckEquals(info, recurrency);

            CheckEmpty_Daily(info);
            CheckEmpty_Monthly(info);
            CheckEmpty_Yearly(info);

            Assert.AreEqual(RecurrencyType.Weekly, info.Type);
        }

        [Test]
        public void Constructor_FromMonthly()
        {
            var recurrency = new MonthlyRecurrency(new DateTime(2011, 10, 7), new DateTime(2012, 4, 7), 2, 14) ;
            var info = new RecurrencyInfo(recurrency);

            CheckEquals_Base(info, recurrency);
            CheckEquals(info, recurrency);

            CheckEmpty_Daily(info);
            CheckEmpty_Weekly(info);
            CheckEmpty_Yearly(info);

            Assert.AreEqual(RecurrencyType.Monthly, info.Type);
        }

        [Test]
        public void Constructor_FromYearly()
        {
            var recurrency = new YearlyRecurrency(new DateTime(2011, 10, 7), new DateTime(2012, 4, 7), 2, 14, 3);
            var info = new RecurrencyInfo(recurrency);

            CheckEquals_Base(info, recurrency);
            CheckEquals(info, recurrency);

            CheckEmpty_Daily(info);
            CheckEmpty_Weekly(info);
            CheckEmpty_Monthly(info);

            Assert.AreEqual(RecurrencyType.Yearly, info.Type);
        }

        [Test]
        public void GetRecurrency_DailyX()
        {
            var info = new RecurrencyInfo { StartDate = new DateTime(2011, 10, 7), Occurrences = 156, DailyType= DailyType.EveryXDays, DailyInterval=50, Type=RecurrencyType.Daily };
            var recurrency = info.GetRecurrency();
            Assert.IsNotNull(recurrency);
            DailyRecurrency daily = recurrency as DailyRecurrency;
            Assert.IsNotNull(daily);
            Assert.AreEqual(new DateTime(2011, 10, 7), daily.StartDate);
            Assert.IsNull(daily.EndDate);
            Assert.AreEqual(156, daily.Occurrences);
            Assert.AreEqual(DailyType.EveryXDays, daily.Type);
            Assert.AreEqual(50, daily.Interval);
        }

        [Test]
        public void GetPattern_Daily()
        {
            var info = new RecurrencyInfo { StartDate = new DateTime(2011, 10, 7), Occurrences = 156, DailyType = DailyType.EveryXDays, DailyInterval = 50, Type = RecurrencyType.Daily };
            var pattern = info.GetPattern();
            Assert.AreEqual('D', pattern[0]);
        }

        [Test]
        public void GetRecurrency_DailyWeekdays()
        {
            var info = new RecurrencyInfo { StartDate = new DateTime(2011, 5, 12), EndDate = new DateTime(2012, 11, 15), DailyType = DailyType.Weekdays, DailyInterval = 50, Type = RecurrencyType.Daily };
            var recurrency = info.GetRecurrency();
            Assert.IsNotNull(recurrency);
            DailyRecurrency daily = recurrency as DailyRecurrency;
            Assert.IsNotNull(daily);

            Assert.AreEqual(new DateTime(2011, 5, 12), daily.StartDate);
            Assert.AreEqual(new DateTime(2012, 11, 15), daily.EndDate);
            Assert.AreEqual(DailyType.Weekdays, daily.Type);
            Assert.AreEqual(1, daily.Interval);
        }

        [Test]
        public void GetRecurrency_Weekly_ByOccurrences()
        {
            var info = new RecurrencyInfo {Type=RecurrencyType.Weekly, StartDate = new DateTime(2011, 5, 12), Occurrences = 156, 
                    WeeklyInterval=50};

            info.WeeklyDays = WeeklyRecurrency.WeekEnd;

            var recurrency = info.GetRecurrency();
            Assert.IsNotNull(recurrency);
            WeeklyRecurrency weekly = recurrency as WeeklyRecurrency;
            Assert.IsNotNull(weekly);

            Assert.AreEqual(new DateTime(2011, 5, 12), weekly.StartDate);
            Assert.IsNull(weekly.EndDate);
            Assert.AreEqual(156, weekly.Occurrences);
            Assert.AreEqual(50, weekly.Interval);
            Assert.IsFalse(weekly.Monday);
            Assert.IsFalse(weekly.Tuesday);
            Assert.IsFalse(weekly.Wednesday);
            Assert.IsFalse(weekly.Thursday);
            Assert.IsFalse(weekly.Friday);
            Assert.IsTrue(weekly.Saturday);
            Assert.IsTrue(weekly.Sunday);
        }

        [Test]
        public void GetRecurrency_Weekly_ByEndDate()
        {
            var info = new RecurrencyInfo
            {
                Type = RecurrencyType.Weekly,
                StartDate = new DateTime(2011, 5, 12),
                EndDate = new DateTime(2011, 10, 6),
                WeeklyInterval = 50
            };

            info.WeeklyDays = WeeklyRecurrency.WeekEnd;
            info.Monday = true;
            info.Tuesday = false;
            info.Wednesday = true;
            info.Thursday = false;
            info.Friday = true;
            info.Saturday = false;
            info.Sunday = false;

            var recurrency = info.GetRecurrency();
            Assert.IsNotNull(recurrency);
            WeeklyRecurrency weekly = recurrency as WeeklyRecurrency;
            Assert.IsNotNull(weekly);

            Assert.AreEqual(new DateTime(2011, 5, 12), weekly.StartDate);
            Assert.AreEqual(new DateTime(2011, 10, 6), weekly.EndDate);
            Assert.AreEqual(0, weekly.Occurrences);
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
        public void GetPattern_Weekly()
        {
            var info = new RecurrencyInfo
            {
                Type = RecurrencyType.Weekly,
                StartDate = new DateTime(2011, 5, 12),
                EndDate = new DateTime(2011, 10, 6),
                WeeklyInterval = 50
            };
            var pattern = info.GetPattern();
            Assert.AreEqual('W', pattern[0]);
        }

        [Test]
        public void GetRecurrency_Monthly_DayOfMonth()
        {
            var info = new RecurrencyInfo
            {
                Type = RecurrencyType.Monthly,
                StartDate = new DateTime(2011, 2, 01),
                EndDate = new DateTime(2011, 11, 30),
                MonthlyInterval = 2,
                MonthlyType = MonthlyType.MonthDay,
                MonthlyDay=14
            };

            var recurrency = info.GetRecurrency();
            Assert.IsNotNull(recurrency);
            MonthlyRecurrency monthly = recurrency as MonthlyRecurrency;
            Assert.IsNotNull(monthly);

            Assert.AreEqual(new DateTime(2011, 2, 01), monthly.StartDate);
            Assert.AreEqual(new DateTime(2011, 11, 30), monthly.EndDate);
            Assert.AreEqual(0, monthly.Occurrences);

            Assert.AreEqual(2, monthly.Interval);
            Assert.AreEqual(MonthlyType.MonthDay, monthly.Type);
            Assert.AreEqual(14, monthly.Day);
        }

        [Test]
        public void GetRecurrency_Monthly_DayOfWeek()
        {
            var info = new RecurrencyInfo
            {
                Type = RecurrencyType.Monthly,
                StartDate = new DateTime(2011, 2, 01),
                Occurrences = 6,
                MonthlyInterval = 2,
                MonthlyType = MonthlyType.Weekday,
                MonthlyDayOfWeek = DayOfWeek.Tuesday,
                MonthlyDayIndex = DayIndex.Third
            };

            var recurrency = info.GetRecurrency();
            Assert.IsNotNull(recurrency);
            MonthlyRecurrency monthly = recurrency as MonthlyRecurrency;
            Assert.IsNotNull(monthly);

            Assert.AreEqual(new DateTime(2011, 2, 01), monthly.StartDate);
            Assert.IsNull(monthly.EndDate);
            Assert.AreEqual(6, monthly.Occurrences);
            Assert.AreEqual(2, monthly.Interval);
            Assert.AreEqual(MonthlyType.Weekday, monthly.Type);
            Assert.AreEqual(01, monthly.Day);
            Assert.AreEqual(DayOfWeek.Tuesday, monthly.DayOfWeek);
            Assert.AreEqual(DayIndex.Third, monthly.DayIndex);
        }

        [Test]
        public void GetPattern_Monthly()
        {
            var info = new RecurrencyInfo
            {
                Type = RecurrencyType.Monthly,
                StartDate = new DateTime(2011, 2, 01),
                Occurrences = 6,
                MonthlyInterval = 2,
                MonthlyType = MonthlyType.Weekday,
                MonthlyDayOfWeek = DayOfWeek.Tuesday,
                MonthlyDayIndex = DayIndex.Third
            };
            var pattern = info.GetPattern();
            Assert.AreEqual('M', pattern[0]);
        }

        [Test]
        public void GetRecurrency_Yearly_DayOfMonth()
        {
            var info = new RecurrencyInfo
            {
                Type = RecurrencyType.Yearly,
                StartDate = new DateTime(2011, 2, 01),
                EndDate = new DateTime(2011, 11, 30),
                YearlyInterval = 2,
                YearlyType = MonthlyType.MonthDay,
                YearlyDay = 14,
                YearlyMonth = 2
            };

            var recurrency = info.GetRecurrency();
            Assert.IsNotNull(recurrency);
            YearlyRecurrency yearly = recurrency as YearlyRecurrency;
            Assert.IsNotNull(yearly);

            Assert.AreEqual(new DateTime(2011, 2, 01), yearly.StartDate);
            Assert.AreEqual(new DateTime(2011, 11, 30), yearly.EndDate);
            Assert.AreEqual(0, yearly.Occurrences);

            Assert.AreEqual(2, yearly.Interval);
            Assert.AreEqual(MonthlyType.MonthDay, yearly.Type);
            Assert.AreEqual(14, yearly.Day);
            Assert.AreEqual(2, yearly.Month);
        }

        [Test]
        public void GetRecurrency_Yearly_DayOfWeek()
        {
            var info = new RecurrencyInfo
            {
                Type = RecurrencyType.Yearly,
                StartDate = new DateTime(2011, 2, 01),
                Occurrences = 6,
                YearlyInterval = 2,
                YearlyType = MonthlyType.Weekday,
                YearlyDayOfWeek = DayOfWeek.Tuesday,
                YearlyDayIndex = DayIndex.Third,
                YearlyMonth = 3
            };

            var recurrency = info.GetRecurrency();
            Assert.IsNotNull(recurrency);
            YearlyRecurrency yearly = recurrency as YearlyRecurrency;
            Assert.IsNotNull(yearly);

            Assert.AreEqual(new DateTime(2011, 2, 01), yearly.StartDate);
            Assert.IsNull(yearly.EndDate);
            Assert.AreEqual(6, yearly.Occurrences);
            Assert.AreEqual(2, yearly.Interval);
            Assert.AreEqual(MonthlyType.Weekday, yearly.Type);
            Assert.AreEqual(01, yearly.Day);
            Assert.AreEqual(DayOfWeek.Tuesday, yearly.DayOfWeek);
            Assert.AreEqual(DayIndex.Third, yearly.DayIndex);
            Assert.AreEqual(3, yearly.Month);
        }

        [Test]
        public void GetPattern_Yearly()
        {
            var info = new RecurrencyInfo
            {
                Type = RecurrencyType.Yearly,
                StartDate = new DateTime(2011, 2, 01),
                Occurrences = 6,
                YearlyInterval = 2,
                YearlyType = MonthlyType.Weekday,
                YearlyDayOfWeek = DayOfWeek.Tuesday,
                YearlyDayIndex = DayIndex.Third,
                YearlyMonth = 3
            };
            var pattern = info.GetPattern();
            Assert.AreEqual('Y', pattern[0]);
        }
    }
}
