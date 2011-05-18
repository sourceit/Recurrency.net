using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
    public class YearlyRecurrency : MonthlyRecurrency
    {
        protected const int _OffsetMonthIndex = _OffsetDayIndex + 1;

        public YearlyRecurrency(string pattern)
            : base(pattern)
        {
            _Month = GetIntFromPattern(_InitialPattern, _OffsetMonthIndex, 2);
        }

        public YearlyRecurrency(DateTime startDate, DateTime endDate, int interval = 1, int dayOfMonth = 1, int month = 1)
            : base(startDate, endDate, interval, dayOfMonth)
        {
            _Month = month;
        }
        public YearlyRecurrency(DateTime startDate, DateTime endDate, int interval = 1, DayOfWeek dayOfWeek = DayOfWeek.Monday, DayIndex dayIndex = DayIndex.First, int month = 1)
            : base(startDate, endDate, interval, dayOfWeek, dayIndex)
        {
            _Month = month;
        }
        public YearlyRecurrency(DateTime startDate, int numOccurrences = _Default_Occurrences, int interval = 1, int dayOfMonth = 1, int month = 1)
            : base(startDate, numOccurrences, interval, dayOfMonth)
        {
            _Month = month;
        }
        public YearlyRecurrency(DateTime startDate, int numOccurrences = _Default_Occurrences, int interval = 1, DayOfWeek dayOfWeek = DayOfWeek.Monday, DayIndex dayIndex = DayIndex.First, int month = 1)
            : base(startDate, numOccurrences, interval, dayOfWeek, dayIndex)
        {
            _Month = month;
        }

        private int _Month;
        public int Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
            }
        }

        public DateTime GetDayInYear(int year)
        {
            DateTime firstInMonth = new DateTime(year, Month, 1);
            return GetDayInMonth(firstInMonth);
        }

        public override DateTime GetFirstDate()
        {
            var day = GetDayInYear(StartDate.Year);
            if (day >= StartDate)
            {
                return day;
            }
            return GetDayInYear(StartDate.Year + 1);
        }

        protected override DateTime? _GetNextFromExact(DateTime knownGood)
        {
            return GetDayInYear(knownGood.Year + Interval);
        }

        public const char TypeCode = 'Y';

        public override char GetTypeCode()
        {
            return TypeCode;
        }

        public override string GetPattern()
        {
            return base.GetPattern() + Month.ToString("00");
        }
    }
}
