using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcRecurrency.Helpers
{
    public static class EnumHelper
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { ID = e, Name = e.ToString().Replace("_", " ") };

            return new SelectList(values, "Id", "Name", enumObj);
        }

    }
}