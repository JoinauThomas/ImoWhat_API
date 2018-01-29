using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Globalization;
using ImmoWhatApp.Helpers;

namespace ImmoWhatApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture; // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        string lang = "fr";
        // GET: Home
        public ActionResult Index()
        {
            
            List<Models.Commune> listeCommunes = new List<Models.Commune>();
            listeCommunes = BLL.CommuneBLL.GetAllCommunesCompleteBLL();

            ViewBag.ListeCommunes = listeCommunes;

            return View();
        }

        public ActionResult MainPage(string nomCommune)
        {
            Models.Commune maCommune = BLL.CommuneBLL.checkIfCommuneExistsBLL(nomCommune);
            return View(maCommune);
        }


        
        

    }
}