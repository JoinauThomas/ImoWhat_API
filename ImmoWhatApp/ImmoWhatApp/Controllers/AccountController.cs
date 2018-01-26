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
        public ActionResult Connexion(Models.Identification moi)
        {
            if(!ModelState.IsValid)
            {
                return View(moi);
            }
            Models.ResultRequest resultRequest = BLL.MembreBLL.Connexion(moi);
            if(resultRequest.msg == "ok")
            {
                Models.MembreModels monProfile = BLL.MembreBLL.GetMyProfile(moi.login);
                Session["moi"] = monProfile;
                return RedirectToAction("Index","Home" );
            }
            else
            {
                if(resultRequest.idError == 65)
                {
                    return View();
                }
                return View("error");
            }

            
        }
    }
}