﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult CreerCompte()
        {

            return View();
        }

        [HttpGet]
        public string CheckIfMailExists(string mail)
        {
            try
            {
                bool result = BLL.MembreBLL.CheckIfMailExists(mail);

                return result.ToString();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            
        }

        [HttpPost]
        public ActionResult CreerCompte(Models.MembreModels newMembre, HttpPostedFileBase monfichier)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(newMembre);
            //}
            if(monfichier == null)
            {
                newMembre.photo = "0";
            }
            Models.ResultInscription resultInscription = new Models.ResultInscription();
           
            resultInscription = BLL.MembreBLL.CreerCompte(newMembre);

            if (newMembre.photo == "1")
            {
                string path = Path.Combine(Server.MapPath("~/img/membre"), "photoMembre" + resultInscription.idMembre.ToString() + ".jpg");
                monfichier.SaveAs(path);
            }

            if (resultInscription.resultQuery == "ok")
            {

                return View();
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
            return PartialView();
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

                Models.ResultRequest resultRequest2 = BLL.MembreBLL.EnregistrerNvlleConnection(moi);
                if (resultRequest2.msg == "ok")
                {
                    return Json(new { result = "OK", nom = monProfile.nom, prenom = monProfile.prenom });
                }
                else
                {
                    return null;
                }


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

        public ActionResult CheckMyProfile()
        {
            Models.MembreModels moi = (Models.MembreModels)Session["moi"];
            
            return View(moi);
        }

        public ActionResult _GetAllMembres(string recherche)
        {
            List<Models.RechercheMembreModels> lesMembres = BLL.MembreBLL.SearchMembres(recherche);

            int nbMembresSupp = lesMembres.FindAll(e => e.deleted == true).Count;
            int nbMembresAdmin = lesMembres.FindAll(e => e.administrator == true).Count;
            int nbMembresActifs = lesMembres.Count - nbMembresSupp - nbMembresAdmin;

            ViewBag.nbMembresSupp = nbMembresSupp;
            ViewBag.nbMembresAdmin = nbMembresAdmin;
            ViewBag.nbMembresActifs = nbMembresActifs;

            return PartialView(lesMembres);
        }

        [HttpPost]
        public Models.RequestResultM DeleteMembreByAdmin(int idMembre)
        {
            try
            {
                Models.RequestResultM resultRequest = BLL.MembreBLL.DeleteMembreByAdmin(idMembre);

                return resultRequest;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            
        }

    }
}