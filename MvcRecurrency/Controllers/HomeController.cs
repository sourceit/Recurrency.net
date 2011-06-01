using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recurrency;

namespace MvcRecurrency.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [HttpGet]
        public ActionResult Index(string pattern)
        {
            RecurrencyInfo info = new RecurrencyInfo(pattern);
            return View(info);
        }

        [HttpPost]
        public ActionResult Index(RecurrencyInfo info)
        {
            // do something with the info object such as save the pattern to database
            return RedirectToAction("Range", new { pattern = info.GetPattern() });
        }

        public ActionResult Range(string pattern)
        {
            RecurrencyInfo info = new RecurrencyInfo(pattern);
            return View(info);
        }
    }
}
