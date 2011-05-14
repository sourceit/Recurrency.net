using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurrency
{
    public abstract class BaseRecurrency
    {
        //public BaseRecurrency(DateTime startDate)
        //{
        //    _StartDate = startDate;
        //    _EndDate = null;
        //    _Occurrences = 10;
        //}

        protected const int _Default_Occurrences = 10;
        protected const int _Max_Occurrences = 999999;

        protected const int _DateLength = 8;
        protected const int _IntLength = 6;
        protected const int _OffsetStartDate = 1;
        protected const int _OffsetEndDate = _OffsetStartDate + _DateLength;
        protected const int _OffsetOccurrences = _OffsetEndDate + _DateLength;
        protected const int _OffsetTypeSpecific = _OffsetOccurrences + _IntLength;

        protected const string _IntFormat = "000000";

        public BaseRecurrency(string pattern)
        {
            _StartDate = GetDateFromPattern(pattern, _OffsetStartDate).Value;
            _EndDate = GetDateFromPattern(pattern, _OffsetEndDate);
            _Occurrences = GetIntFromPattern(pattern, _OffsetOccurrences);
        }

        public BaseRecurrency(DateTime startDate, DateTime endDate)
        {
            _StartDate = startDate;
            _EndDate = endDate;
            _Occurrences = 0;
        }

        public BaseRecurrency(DateTime startDate, int numOccurrences = _Default_Occurrences)
        {
            _StartDate = startDate;
            _EndDate = null;
            _Occurrences = numOccurrences;
        }

        private DateTime _StartDate;
        public DateTime StartDate 
        {
            get
            {
            	return _StartDate; 
            }
        }

        private DateTime? _EndDate;
        public DateTime? EndDate
        {
            get
            {
                return _EndDate;
            }
        }

        private int _Occurrences;
        public int Occurrences
        {
            get
            {
                return _Occurrences;
            }
        }

        public static DateTime? GetDateFromPattern(string pattern, int startPos)
        {
            var sub = pattern.Substring(startPos, 8);
            if(sub == "00000000") { return null; }


            return new DateTime(GetIntFromPattern(sub, 0, 4), GetIntFromPattern(sub, 4, 2), GetIntFromPattern(sub, 6, 2));
        }

        public static int GetIntFromPattern(string pattern, int startPos, int length = _IntLength)
        {
            var sub = pattern.Substring(startPos, length);
            return Int32.Parse(sub);
        }

        public static string DateToPatternFormat(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("yyyyMMdd") : "00000000";
        }

        protected virtual DateTime? _GetNextFromExact(DateTime knownGood)
        {
            throw new NotImplementedException();
        }

        //protected virtual DateTime? _GetNextFromExact(DateTime knownGood, int index)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual DateTime GetFirstDate()
        {
            throw new NotImplementedException();
        }

        public virtual DateTime? GetNextDate(DateTime current)
        {
            // default (simple) implimentation

            var max = _Occurrences == 0 ? _Max_Occurrences : _Occurrences;
            DateTime? next = GetFirstDate();
            for (int i = 1; i <= max; i++)
            {
                if (_EndDate.HasValue && next > _EndDate) { return null; }
                if (next > current || next == null)
                {
                    return next;
                }
                next = _GetNextFromExact(next.Value);
            }
            return null;
        }

        public virtual string GetPattern()
        {
            throw new NotImplementedException();
        }

        public string GetInitialPattern()
        {
            return string.Format("{0}{1}{2}", DateToPatternFormat(_StartDate), DateToPatternFormat(_EndDate), _Occurrences.ToString(_IntFormat));
        }
    }
}
