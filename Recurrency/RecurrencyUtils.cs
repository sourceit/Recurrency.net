using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
    public static class RecurrencyUtils
    {
        public static BaseRecurrency Create(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException("Invalid pattern supplied: " + pattern ?? "");
            }

            switch (pattern[0])
            {
                case DailyRecurrency.TypeCode: return new DailyRecurrency(pattern);
                case WeeklyRecurrency.TypeCode: return new WeeklyRecurrency(pattern);
                case MonthlyRecurrency.TypeCode: return new MonthlyRecurrency(pattern);
                case YearlyRecurrency.TypeCode: return new YearlyRecurrency(pattern);
            }

            throw new ArgumentNullException("Invalid pattern supplied: " + pattern ?? "");
        }

        public static string GetDescription(string pattern)
        {
            return Create(pattern).ToString();
        }
    }
}
