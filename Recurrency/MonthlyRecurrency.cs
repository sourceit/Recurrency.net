using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
    public enum MonthlyType { MonthDay, Weekday }
    
    public class MonthlyRecurrency : BaseRecurrency
    {

        protected const int _OffsetDay = _OffsetTypeSpecific + 1;
        protected const int _OffsetDayIndex = _OffsetDay + 2;

        public MonthlyRecurrency(string pattern)
            : base(pattern)
        {
            SetTypeAndDays(_InitialPattern[_OffsetTypeSpecific] == 'M' ? MonthlyType.MonthDay : MonthlyType.Weekday, 
                GetIntFromPattern(_InitialPattern, _OffsetDay, 2), 
                GetIntFromPattern(_InitialPattern, _OffsetDayIndex, 1));
        }

        public MonthlyRecurrency(DateTime startDate, DateTime endDate, int interval = 1, int dayOfMonth = 1)
            : base(startDate, endDate, interval)
        {
            SetTypeAndDays(MonthlyType.MonthDay, dayOfMonth, 0);
        }

        public MonthlyRecurrency(DateTime startDate, DateTime endDate, int interval = 1, DayOfWeek dayOfWeek = DayOfWeek.Monday, DayIndex dayIndex = DayIndex.First)
            : base(startDate, endDate, interval)
        {
            SetTypeAndDays(MonthlyType.Weekday, dayOfWeek, dayIndex);
        }

        public MonthlyRecurrency(DateTime startDate, int numOccurrences = _Default_Occurrences, int interval = 1, int dayOfMonth = 1)
            : base(startDate, numOccurrences, interval)
        {
            SetTypeAndDays(MonthlyType.MonthDay, dayOfMonth, 0);
        }

        public MonthlyRecurrency(DateTime startDate, int numOccurrences = _Default_Occurrences, int interval = 1, DayOfWeek dayOfWeek = DayOfWeek.Monday, DayIndex dayIndex = DayIndex.First)
            : base(startDate, numOccurrences, interval)
        {
            SetTypeAndDays(MonthlyType.Weekday, dayOfWeek, dayIndex);
        }

        private void SetTypeAndDays(MonthlyType type, int day, int index)
        {
            _Type = type;
            _Day = day;
            _Index = index;
        }

        private void SetTypeAndDays(MonthlyType type, DayOfWeek dayOfWeek, DayIndex dayIndex)
        {
            _Type = type;
            DayOfWeek = dayOfWeek;
            DayIndex = dayIndex;
        }

        private MonthlyType _Type;
        public MonthlyType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
            }
        }

        private int _Day;
        public int Day
        {
            get { return _Day; }
            set
            {
                _Day = value;
            }
        }

        public DayOfWeek DayOfWeek
        {
            get { return  _Day >= 6 ? DayOfWeek.Sunday : (DayOfWeek)_Day + 1; }
            set
            {
                _Day = value == DayOfWeek.Sunday ? 6 : (int)value - 1;
            }
        }

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
            }
        }

        public DayIndex DayIndex
        {
            get { return  (DayIndex)_Index; }
            set
            {
                _Index = (int)value;
            }
        }

        public DateTime GetDayInMonth(DateTime month)
        {
            if (Type == MonthlyType.MonthDay)
            {
                return month.ChangeDay(Day);
            }
            else
            {
                return month.ChangeDay(DayOfWeek, DayIndex);
            }
        }

        public override DateTime GetFirstDate()
        {
            var day = GetDayInMonth(StartDate);
            if (day >= StartDate) 
            { 
                return day; 
            }
            return GetDayInMonth(StartDate.AddMonths(1));
        }

        protected override DateTime? _GetNextFromExact(DateTime knownGood)
        {
            return GetDayInMonth(knownGood.FirstDayOfMonth().AddMonths(Interval));
        }
    }
}
