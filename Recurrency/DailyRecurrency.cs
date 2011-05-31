using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
    public enum DailyType { EveryXDays, Weekdays }

    public class DailyRecurrency : BaseRecurrency
    {
        
        public DailyRecurrency(string pattern)
            : base(pattern)
        {
            if (_InitialPattern[_OffsetTypeSpecific] == 'W')
            {
                Type = DailyType.Weekdays;
                Interval = 1;
            }
            else
            {
                Type = DailyType.EveryXDays;
            }
        }

        public DailyRecurrency(DateTime startDate, DateTime endDate, DailyType type = DailyType.EveryXDays, int interval = 1)
            : base(startDate, endDate)
        {
            SetTypeAndInterval(type, interval);
        }

        public DailyRecurrency(DateTime startDate, int numOccurrences = _Default_Occurrences, DailyType type = DailyType.EveryXDays, int interval = 1)
            : base(startDate, numOccurrences)
        {
            SetTypeAndInterval(type, interval);
        }        

        private void SetTypeAndInterval(DailyType type, int interval)
        {
            Type = type;
            Interval = type == DailyType.EveryXDays ? interval : 1;  // currently only support intervals of 1 for weekdays
        }

        
        public DailyType Type { get; set; }

        //                               s  m  t  w  t  f  s
        private int[] _FirstDayOffset = {1, 0, 0, 0, 0, 0, 2 };
        private int[] _NextDayOffset = { 1, 1, 1, 1, 1, 3, 2 };

        protected override DateTime? _GetNextFromExact(DateTime knownGood)
        {
            switch (Type)
            {
                case DailyType.EveryXDays: return knownGood.AddDays(Interval);
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
            return string.Format("{0}{1}", GetInitialPattern(), Type == DailyType.Weekdays ? 'W' : 'X');
        }

        public const char TypeCode = 'D';
 
        public override char GetTypeCode()
        {
            return TypeCode;
        }

        private string GetTypeName()
        {
            switch (Type)
            {
                case DailyType.EveryXDays: return "day";
                case DailyType.Weekdays: return "weekday";
                default: throw new Exception("Unknown daily type");
            }
        }

        public override string ToString()
        {

            return string.Format("{0} {1}", ToStringPrefix(GetTypeName()), ToStringSuffix());
        }

        public override RecurrencyType GetType()
        {
            return RecurrencyType.Daily;
        }
    }
}
