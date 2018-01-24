using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        public ActionResult CreerCompte(Models.MembreModels newMembre)
        {
            //System.Security.Cryptography.MD5CryptoServiceProvider test = new System.Security.Cryptography.MD5CryptoServiceProvider();
            //byte[] data = System.Text.Encoding.ASCII.GetBytes(newMembre.pswd);
            //data = test.ComputeHash(data);
            //String md5Hash = System.Text.Encoding.ASCII.GetString(data);
            //newMembre.pswd = md5Hash;

            string Result = BLL.MembreBLL.CreerCompte(newMembre);


            if (Result == "ok")
            {
                return RedirectToAction("nvBien");
            }
            else
            {
                ViewBag.Error = Result;
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
            string resultat = BLL.MembreBLL.Connexion(moi);
            if(resultat == "ok")
            {
                return View("index");
            }
            else
            {
                return View("error");
            }

            
        }
    }
}