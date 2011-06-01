using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recurrency;

namespace MvcRecurrency.Helpers
{
    public static class RecurrencyInfoHelper
    {
        public static IEnumerable<SelectListItem> MonthSelectList(this RecurrencyInfo info)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = new DateTime(2000, i, 1).ToString("MMMM"),
                    Value = i.ToString(),
                    Selected = i == info.YearlyMonth
                });
            }

            return items;
        }
    }
}