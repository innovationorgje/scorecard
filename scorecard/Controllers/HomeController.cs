using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using scorecard.Data;
using scorecard.Models;

namespace scorecard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RAGContext context = new RAGContext();

            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Groups = context.Groups.ToList();
            
            return View(model);
        }
    }
}