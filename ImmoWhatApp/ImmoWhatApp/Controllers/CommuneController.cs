using ImmoWhatApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ImmoWhatApp.Controllers
{
    public class CommuneController : BaseController
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

        [HttpPost]
        public ActionResult mesCommunes()
        {
            List<Models.Commune> Communes = new List<Models.Commune>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49383/api/Commune/");
                var responseTask = client.GetAsync("mesCommunes");
                var result = responseTask.Result;
                responseTask.Wait();

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Models.Commune>>();
                    readTask.Wait();
                    var maListe = readTask.Result;
                    Communes = maListe.ToList();
                }
                else
                {
                    var content = result.Content.ReadAsStringAsync();
                    content.Wait();
                    ModelState.AddModelError(string.Empty, "Server error : " + content.Result);
                }
            }
            return View(Communes);
        }
        [HttpGet]
        public JsonResult GetCommunesInJson()
        {
            IList<Models.Commune> ListeDeCommunes = BLL.CommuneBLL.GetAllCommunesCompleteBLL();
            List<string> CommuneStr = new List<string>();

            foreach (var i in ListeDeCommunes)
            {
                CommuneStr.Add(i.ToString());
            }

            return Json(new { result = "OK", commune = ListeDeCommunes }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetCommunesByNameInJson(string nom)
        {
            List<Models.Commune> ListeDeCommunes = BLL.CommuneBLL.GetAllCommunesCompleteBLL();
            List<Models.Commune> CommuneByName = new List<Models.Commune>();

            CommuneByName = ListeDeCommunes.FindAll(x => (x.Localite.Contains(nom) || x.CodePostal.StartsWith(nom)) && x.langue == lang).Take(5).ToList();

            List<string> CommuneStr = new List<string>();

            foreach (var i in ListeDeCommunes)
            {
                CommuneStr.Add(i.ToString());
            }

            return Json(new { result = "OK", commune = CommuneByName }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult checkIfCommuneExists(string nomCommune)
        {
            Models.Commune result = BLL.CommuneBLL.checkIfCommuneExistsBLL(nomCommune);
            if(result != null)
            {
                return Json(new { result = "OK", commune = result.id, nom = result.Localite, tt = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }
    }
}