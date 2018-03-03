using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult CreerCompte()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreerCompte(Models.MembreModels newMembre, HttpPostedFileBase monfichier)
        {
            if (!ModelState.IsValid)
            {
                return View(newMembre);
            }
            if(monfichier == null)
            {
                newMembre.photo = "0";
            }
            Models.ResultInscription resultInscription = new Models.ResultInscription();
           
            resultInscription = BLL.MembreBLL.CreerCompte(newMembre);

            if (monfichier != null && monfichier.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/img/membre"), "photoMembre" + resultInscription.idMembre.ToString() + ".jpg");
                monfichier.SaveAs(path);
            }

            if (resultInscription.resultQuery == "ok")
            {

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = resultInscription.resultQuery;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Connexion()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Connexion(string login, string password)
        {
            Models.Identification moi = new Models.Identification { login = login, password = password };
            
            Models.ResultRequest resultRequest = BLL.MembreBLL.Connexion(moi);
            if(resultRequest.msg == "ok")
            {
                Models.MembreModels monProfile = BLL.MembreBLL.GetMyProfile(moi.login);
                Session["moi"] = monProfile;
                


                return Json(new { result = "OK", nom = monProfile.nom, prenom = monProfile.prenom });
            }
            else
            {
                return null;
            }

            
        }

        public JsonResult deconnexion()
        {
            try
            {
               
                Models.MembreModels moi = (Models.MembreModels)Session["moi"];
                Session["monToken"] = null;
                Session["moi"] = null;

                HttpCookie myToken;
                string cookieName;
                int limit = Request.Cookies.Count;
                for (int i = 0; i < limit; i++)
                {
                    cookieName = Request.Cookies[i].Name;
                    myToken = new HttpCookie(cookieName);
                    myToken.Value = "";
                    Response.Cookies.Add(myToken);
                }


                return Json(new { result = "OK", nom = moi.nom, prenom = moi.prenom });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}