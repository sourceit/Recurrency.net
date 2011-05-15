using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
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
    }
}
