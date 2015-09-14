using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using scorecard.Data;

namespace scorecard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RAGContext context = new RAGContext();
            return View(context.Groups.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}