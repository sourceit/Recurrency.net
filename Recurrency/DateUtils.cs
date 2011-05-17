using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
    public enum DayIndex { First, Second, Third, Fourth, Last }

    public static class DateUtils
    {
        public static int DayOfWeek_Mondayised(this DateTime date)
        {
            int day = (int)date.DayOfWeek;

            return day == 0 ? 6 : day -1;            
        }

        public static DateTime FirstDayOfWeek_Mondayised(this DateTime date)
        {
            return date.AddDays(-date.DayOfWeek_Mondayised());            
        }

        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return date.ChangeDay(1);
        }

        public static DateTime ChangeDay(this DateTime date, int dayOfMonth)
        {
            int day = date.Day;
            return date.AddDays(dayOfMonth - day);
        }


        public static DateTime ChangeDay(this DateTime date, DayOfWeek day, DayIndex index)
        {
            
            var firstOfMonth = FirstDayOfMonth(date);

            int offset = (int)day - (int)firstOfMonth.DayOfWeek;
            if (offset < 0) { offset += 7; }

            var newDate = firstOfMonth.AddDays(offset + 7 * (int)index);

            if (newDate.Month != date.Month) 
            {
                // must be last in month (else wouldn't have gone over, so go back one week
                newDate = newDate.AddDays(-7);
            }
            return newDate;
        }
    }
}
