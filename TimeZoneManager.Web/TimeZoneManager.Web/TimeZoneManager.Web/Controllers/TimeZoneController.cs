using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeZoneManager.Web.Controllers
{
    public class TimeZoneController : Controller
    {
        // GET: TimeZone
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}