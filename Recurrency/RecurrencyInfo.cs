using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
    public class RecurrencyInfo
    {
        public RecurrencyInfo()
        {
            Reset();
        }

        public RecurrencyInfo(string pattern)
        {
            Reset(pattern);
        }

        public RecurrencyInfo(BaseRecurrency recurrency)
        {
            Reset(recurrency);
        }


        public void Reset(string pattern)
        {
            Reset(RecurrencyUtils.Create(pattern));
        }

        public void Reset(BaseRecurrency recurrency)
        {
            Reset();
            SetBaseValues(recurrency);

            if (recurrency != null)
            {
                switch (recurrency.GetType())
                {
                    case RecurrencyType.Daily:
                        SetDailyValues(recurrency as DailyRecurrency);
                        break;
                    case RecurrencyType.Weekly:
                        SetWeeklyValues(recurrency as WeeklyRecurrency);
                        break;
                    case RecurrencyType.Monthly:
                        SetMonthlyValues(recurrency as MonthlyRecurrency);
                        break;
                    case RecurrencyType.Yearly:
                        SetYearlyValues(recurrency as YearlyRecurrency);
                        break;
                    default:
                        throw new ArgumentException("Unknown recurrency type");

                }

                Type = recurrency.GetType();
            }
        }

        public void Reset()
        {
            var daily = new DailyRecurrency(DateTime.Today);
            SetBaseValues(daily);
            SetDailyValues(daily);

            SetWeeklyValues(new WeeklyRecurrency(DateTime.Today) { Days = WeeklyRecurrency.WeekDays });
            SetMonthlyValues(new MonthlyRecurrency(DateTime.Today, dayOfMonth: 1));
            SetYearlyValues(new YearlyRecurrency(DateTime.Today, dayOfMonth: 1));

            Type = RecurrencyType.Daily; 
        }

        private void SetBaseValues(BaseRecurrency recurrency)
        {
            StartDate = recurrency.StartDate;
            EndDate = recurrency.EndDate;
            Occurrences = recurrency.Occurrences;
        }

        private void SetDailyValues(DailyRecurrency recurrency)
        {
            DailyInterval = recurrency.Interval;
            DailyType = recurrency.Type;
        }

        private void SetWeeklyValues(WeeklyRecurrency recurrency)
        {
            WeeklyInterval = recurrency.Interval;
            WeeklyDays = recurrency.Days;
        }

        private void SetMonthlyValues(MonthlyRecurrency recurrency)
        {
            MonthlyInterval = recurrency.Interval;

            MonthlyType = recurrency.Type;
            MonthlyDay = recurrency.Day;
            MonthlyDayOfWeek = recurrency.DayOfWeek;
            MonthlyDayIndex = recurrency.DayIndex;
        }

        private void SetYearlyValues(YearlyRecurrency recurrency)
        {
            YearlyInterval = recurrency.Interval;
            YearlyType= recurrency.Type;
            YearlyDay= recurrency.Day;
            YearlyDayOfWeek= recurrency.DayOfWeek;
            YearlyDayIndex= recurrency.DayIndex;
            YearlyMonth = recurrency.Month;
        }

        public BaseRecurrency GetRecurrency()
        {
            switch (Type)
            {
                case RecurrencyType.Daily: return GetDailyRecurrency();
                case RecurrencyType.Weekly: return GetWeeklyRecurrency();
                case RecurrencyType.Monthly: return GetMonthlyRecurrency();
                case RecurrencyType.Yearly: return GetYearlyRecurrency();
                default: throw new ArgumentException("Unknown recurrency type");
            }
        }

        public string GetPattern()
        {
            return GetRecurrency().GetPattern();
        }

        private DailyRecurrency GetDailyRecurrency()
        {
            if (EndDate.HasValue)
            {
                return new DailyRecurrency(StartDate, EndDate.Value, DailyType, DailyInterval);
            }
            return new DailyRecurrency(StartDate, Occurrences, DailyType, DailyInterval); ;
        }

        private WeeklyRecurrency GetWeeklyRecurrency()
        {
            if (EndDate.HasValue)
            {
                return new WeeklyRecurrency(StartDate, EndDate.Value, WeeklyInterval, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday);
            }
            return new WeeklyRecurrency(StartDate, Occurrences, WeeklyInterval, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday); ;
        }

        private MonthlyRecurrency GetMonthlyRecurrency()
        {
            MonthlyRecurrency monthly = EndDate.HasValue ? new MonthlyRecurrency(StartDate, EndDate.Value, MonthlyInterval, 1) : new MonthlyRecurrency(StartDate, Occurrences, MonthlyInterval, 1);
            monthly.Type = MonthlyType;
            if (MonthlyType == Recurrency.MonthlyType.MonthDay)
            {
                monthly.Day = MonthlyDay;
            }
            else
            {
                monthly.DayIndex = MonthlyDayIndex;
                monthly.DayOfWeek = MonthlyDayOfWeek;
            }
            return monthly;
        }

        private YearlyRecurrency GetYearlyRecurrency()
        {
            YearlyRecurrency yearly = EndDate.HasValue ? new YearlyRecurrency(StartDate, EndDate.Value, YearlyInterval, 1, YearlyMonth) : new YearlyRecurrency(StartDate, Occurrences, YearlyInterval, 1, YearlyMonth);
            yearly.Type = YearlyType;
            if (YearlyType == MonthlyType.MonthDay)
            {
                yearly.Day = YearlyDay;
            }
            else
            {
                yearly.DayIndex = YearlyDayIndex;
                yearly.DayOfWeek = YearlyDayOfWeek;
            }
            return yearly;
        }

        #region Base Recurrency Properties
        
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Occurrences { get; set; }
        
        #endregion

        public RecurrencyType Type { get; set; }

        #region Daily Recurency Properties

        public DailyType DailyType { get; set; }
        public int DailyInterval { get; set; }
        #endregion

        #region Weekly Recurrency Properties
        private bool[] _Days = { false, false, false, false, false, false, false };

        public bool[] WeeklyDays
        {
            get { return _Days; }
            set
            {
                if (value == null || value.Length < 7) { throw new ArgumentException("Insufficient days.  There must be at least 7 days in the array."); }
                for (int i = 0; i < 7; i++)
                {
                    _Days[i] = value[i];
                }
            }
        }

        public int WeeklyInterval { get; set; }

        public bool Monday
        {
            get { return _Days[0]; }
            set
            {
                _Days[0] = value;
            }
        }

        public bool Tuesday
        {
            get { return _Days[1]; }
            set
            {
                _Days[1] = value;
            }
        }

        public bool Wednesday
        {
            get { return _Days[2]; }
            set
            {
                _Days[2] = value;
            }
        }

        public bool Thursday
        {
            get { return _Days[3]; }
            set
            {
                _Days[3] = value;
            }
        }

        public bool Friday
        {
            get { return _Days[4]; }
            set
            {
                _Days[4] = value;
            }
        }

        public bool Saturday
        {
            get { return _Days[5]; }
            set
            {
                _Days[5] = value;
            }
        }

        public bool Sunday
        {
            get { return _Days[6]; }
            set
            {
                _Days[6] = value;
            }
        }
        #endregion

        #region Monthly Recurrency Properties

        public int MonthlyInterval { get; set; }

        public MonthlyType MonthlyType { get; set; }

        public int MonthlyDay { get; set; }

        public DayOfWeek MonthlyDayOfWeek { get; set; }
        public DayIndex  MonthlyDayIndex { get; set; } 
        #endregion

        #region Yearly Recurrency Properties

        public int YearlyInterval { get; set; }

        public MonthlyType YearlyType { get; set; }

        public int YearlyDay { get; set; }


        public DayOfWeek YearlyDayOfWeek { get; set; }
        public DayIndex YearlyDayIndex { get; set; }

        public int YearlyMonth { get; set; }
        #endregion
    }
}
