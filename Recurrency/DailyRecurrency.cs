using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
    public enum DailyType { EveryXDays, Weekdays }

    public class DailyRecurrency : BaseRecurrency
    {
        protected const int _OffsetDaysApart = _OffsetTypeSpecific + 1;

        public DailyRecurrency(string pattern)
            : base(pattern)
        {
            if (pattern[_OffsetTypeSpecific] == 'W')
            {
                Type = DailyType.Weekdays;
                DaysApart = 0;
            }
            else
            {
                Type = DailyType.EveryXDays;
                DaysApart = GetIntFromPattern(pattern, _OffsetDaysApart);
            }
        }

        public DailyRecurrency(DateTime startDate, DateTime endDate, DailyType type = DailyType.EveryXDays, int daysApart = 1)
            : base(startDate, endDate)
        {
            SetTypeAndDaysApart(type, daysApart);
        }

        public DailyRecurrency(DateTime startDate, int numOccurrences = _Default_Occurrences, DailyType type = DailyType.EveryXDays, int daysApart = 1)
            : base(startDate, numOccurrences)
        {
            SetTypeAndDaysApart(type, daysApart);
        }

        private void SetTypeAndDaysApart(DailyType type, int daysApart)
        {
            Type = type;
            DaysApart = type == DailyType.EveryXDays ? daysApart : 1;
        }

        
        public DailyType Type { get; set; }

        private int _DaysApart;
        public int DaysApart
        {
            get { return _DaysApart; }
            set
            {
                _DaysApart = value;
            }
        }

        //                               s  m  t  w  t  f  s
        private int[] _FirstDayOffset = {1, 0, 0, 0, 0, 0, 2 };
        private int[] _NextDayOffset = { 1, 1, 1, 1, 1, 3, 2 };

        protected override DateTime? _GetNextFromExact(DateTime knownGood)
        {
            switch (Type)
            {
                case DailyType.EveryXDays: return knownGood.AddDays(_DaysApart);
                case DailyType.Weekdays: return knownGood.AddDays(_NextDayOffset[(int)knownGood.DayOfWeek]);
                default: throw new Exception("Unknown daily type");
            }
        }

        public override DateTime GetFirstDate()
        {
            switch (Type)
            {
                case DailyType.EveryXDays: return StartDate;
                case DailyType.Weekdays: return StartDate.AddDays(_FirstDayOffset[(int)StartDate.DayOfWeek]);
                default: throw new Exception("Unknown daily type");
            }
        }

        public override string GetPattern()
        {
            if (Type == DailyType.Weekdays)
            {
                return string.Format("D{0}W", GetInitialPattern());
            }
            return string.Format("D{0}X{1}", GetInitialPattern(), DaysApart.ToString(_IntFormat));

        }
    }
}
