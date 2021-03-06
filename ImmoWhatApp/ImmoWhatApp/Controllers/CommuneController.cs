﻿using ImmoWhatApp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public static Models.Commune GetACommuneWithCodePostal(string codePostal)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant();
            if (langue == "en")
            {
                langue = "fr";
            }
            Models.Commune maCommune = BLL.CommuneBLL.GetACommuneWithCodePostal(codePostal, langue);

            return maCommune;
        }
        [HttpGet]
        public JsonResult GetCommunesInJson()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant();
            IList<Models.Commune> ListeDeCommunes = BLL.CommuneBLL.GetAllCommunesCompleteWithLanguageBLL(langue);
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
            var langue = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant();
            if(langue == "en")
            {
                langue = "fr";
            }
            List<Models.Commune> ListeDeCommunes = BLL.CommuneBLL.GetAllCommunesCompleteBLL();
            List<Models.Commune> CommuneByName = new List<Models.Commune>();

            CommuneByName = ListeDeCommunes.FindAll(x => (x.Localite.Contains(nom) || x.CodePostal.StartsWith(nom)) && x.langue == langue).Take(5).ToList();

            List<string> CommuneStr = new List<string>();

            //foreach (var i in ListeDeCommunes)
            //{
            //    CommuneStr.Add(i.ToString());
            //}

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
        
        [HttpGet]
        public static int GetAveragePrice (int annee, string typeBien, string codePostale)
        {
            int moyenne = BLL.CommuneBLL.GetAveragePrice(annee, typeBien, codePostale);

            return moyenne;
        }

        [HttpGet]
        public JsonResult GetAverageClass(int annee, string typeBien, string codePostale)
        {
            int classePrix = BLL.CommuneBLL.GetAverageClass(annee, typeBien, codePostale);

            return Json(new { result = "OK", classe = classePrix}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAverageClass2(int annee, string typeBien, string codePostale)
        {
            List<Models.TablePrice> listePrix = BLL.CommuneBLL.GetTablePrice(typeBien);
            int classePrix = BLL.CommuneBLL.GetIdClassPrix(annee, typeBien, codePostale);
            int prixMoyen = BLL.CommuneBLL.GetAveragePrice(annee, typeBien, codePostale);

            return Json(new { result = "OK", tablePrix = listePrix, classePrix = classePrix, prixMoyen = prixMoyen }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTableStatPrixCommune(string codePostal)
        {
            List<Models.tableStatModels> mesStats = new List<Models.tableStatModels>();
            mesStats = BLL.CommuneBLL.GetInfoEvolutionPrices(codePostal);

            ViewBag.liste = mesStats;

            return View(mesStats);
        }

        [HttpGet]
        public string GetCommuneContourPointsInJson(int idType)
        {
            //Models.CommuneContourPoint ListePoints = new Models.CommuneContourPoint();
            //ListePoints = BLL.CommuneBLL.GetCommuneContourPoints(idType);
            string ListePoints = "";
            switch (idType)
            {
                case 1:
                    ListePoints = (string)HttpContext.Cache["JsonContourterrain"];
                    break;
                case 2:
                    ListePoints = (string)HttpContext.Cache["JsonContourMaison"];
                    break;
                case 3:
                    ListePoints = (string)HttpContext.Cache["JsonContourVilla"];
                    break;
                case 4:
                    ListePoints = (string)HttpContext.Cache["JsonContourappartement"];
                    break;
            }
            

            //string jsonData = JsonConvert.SerializeObject(ListePoints);
            return ListePoints;
        }

        [HttpGet]
        public JsonResult GetInfosCommune(string commune, int age)
        {
            
            Models.StatCommune statCommune = BLL.CommuneBLL.GetInfosCommune(commune, age);

            return Json(new { result = "OK", stat = statCommune, listPrenoms = statCommune.listePrenoms }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public static string GetCommuneContourPointsInJson2(int idType)
        {
            Models.CommuneContourPoint ListePoints = new Models.CommuneContourPoint();
            ListePoints = BLL.CommuneBLL.GetCommuneContourPoints(idType);

            string jsonData = JsonConvert.SerializeObject(ListePoints);
            return jsonData;
        }





    }
}