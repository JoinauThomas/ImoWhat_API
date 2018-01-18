using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;

namespace ImmoWhatApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Models.Commune> listeCommunes = new List<Models.Commune>();
            listeCommunes = BLL.CommuneBLL.GetAllCommunesCompleteBLL();

            ViewBag.ListeCommunes = listeCommunes;

            return View();
        }

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

            CommuneByName = ListeDeCommunes.FindAll(x => x.Localite.Contains(nom)).Take(5).ToList();

            List<string> CommuneStr = new List<string>();

            foreach (var i in ListeDeCommunes)
            {
                CommuneStr.Add(i.ToString());
            }

            return Json(new { result = "OK", commune = CommuneByName }, JsonRequestBehavior.AllowGet);
        }

    }
}