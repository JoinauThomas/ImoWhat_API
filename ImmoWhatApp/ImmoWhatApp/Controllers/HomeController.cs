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
            {
                cookie.Value = culture; // update cookie value
            }
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);

            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        public int GetHighestYear()
        {
            int annee = BLL.HomeBLL.GetHighestYear();
            return annee;
        }
        public void GetMinYear(string codePostal)
        {
            int annee = BLL.HomeBLL.GetMinYear(codePostal);
            Session["anneeMin"] = annee;
        }
        
        // GET: Home
        public ActionResult Index()
        {
            Session["HighestYear"] = GetHighestYear();
            var langue = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant();
            List<Models.Commune> listeCommunes = new List<Models.Commune>();
            listeCommunes = BLL.CommuneBLL.GetAllCommunesCompleteWithLanguageBLL(langue);

            ViewBag.ListeCommunes = listeCommunes;

            return View();
        }

        public ActionResult MainPage(string nomCommune)
        {
            
            Models.Commune maCommune = BLL.CommuneBLL.checkIfCommuneExistsBLL(nomCommune);
            return View(maCommune);
        }

        public ActionResult Graphic (string codePostal, string commune, string lat, string lon, int id, string langue, string province, string bouton)
        {
            GetMinYear(codePostal);
            Models.Commune maCommune = new Models.Commune{ CodePostal = codePostal, latitude = lat, longitude = lon, Localite = commune, id = id, langue = langue, Province = province };
            ViewBag.bouton = bouton;

            return View(maCommune);
        }


        
        

    }
}