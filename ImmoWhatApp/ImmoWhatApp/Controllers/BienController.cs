using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.Controllers
{
    public class BienController : Controller
    {
        // GET: Bien
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddNewBien()
        {
            return View();
        }
    }
}