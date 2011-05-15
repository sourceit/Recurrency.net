using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
    public class WeeklyRecurrency : BaseRecurrency
    {
        public WeeklyRecurrency(string pattern)
            : base(pattern)
        {
            for (int i = 0; i < 7; i++)
            {
                _Days[i] = _InitialPattern[_OffsetTypeSpecific + i] == 'Y';
            }
        }

        public WeeklyRecurrency(DateTime startDate, DateTime endDate, int interval = 1, bool monday= false,
                                                                                        bool tuesday= false,
                                                                                        bool wednesday= false,
                                                                                        bool thursday= false,
                                                                                        bool friday= false,
                                                                                        bool saturday= false,
                                                                                        bool sunday = false)
            : base(startDate, endDate, interval)
        {
            Sunday = sunday;
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
        }

        public WeeklyRecurrency(DateTime startDate, int numOccurrences = _Default_Occurrences, int interval = 1, bool monday = false,
                                                                                                                 bool tuesday = false,
                                                                                                                 bool wednesday = false,
                                                                                                                 bool thursday = false,
                                                                                                                 bool friday = false,
                                                                                                                 bool saturday = false,
                                                                                                                 bool sunday = false)
            : base(startDate, numOccurrences, interval)
        {
            Sunday = sunday;
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
        }

        private bool[] _Days = { false, false, false, false, false, false, false };
        public static bool[] WeekDays = { true, true, true, true, true, false, false };
        public static bool[] WeekEnd = { false, false, false, false, false, true, true };
        public static bool[] EveryDay = { true, true, true, true, true, true, true };

        public bool[] Days
        {
            get { return _Days; }
            set
            {
                if(value == null || value.Length < 7) { throw new ArgumentException("Insufficient days.  There must be at least 7 days in the array."); }
                _Days = value;
            }
        }

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

        
    }
}
