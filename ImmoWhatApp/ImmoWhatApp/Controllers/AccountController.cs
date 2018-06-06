using Newtonsoft.Json.Linq;
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

        public ActionResult CheckMyProfile()
        {
            Models.MembreModels moi = (Models.MembreModels)Session["moi"];
            
            return View(moi);
        }

        [HttpGet]
        public JsonResult GetCountOfNewMails(int idMembre)
        {
            try
            {
                Models.RequestResultM resuleRequest = BLL.MembreBLL.GetCountOfNewMails(idMembre);

                if (resuleRequest.result == "OK")
                    return Json(new { result = "OK", nbNvMsg = resuleRequest.resultRequest }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { result = "NoOK", msg = resuleRequest.msg }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public JsonResult SendMail(string adresseMail, string msg, int idProprietaire, string sujet)
        {
            try
            {
                Models.Mail newMail = new Models.Mail{ adresseMail = adresseMail, message = msg, idProprietaire = idProprietaire, sujet = sujet };

                Models.RequestResultM resultRequest = BLL.MembreBLL.sendMail(newMail);

                if(resultRequest.result == "ok")
                    return Json(new { result = "OK", msg = resultRequest.msg });
                else
                    return Json(new { result = "NoOK", msg = resultRequest.msg });


            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void envoiMail()
        {
            envoiMail2();
        }

        protected void envoiMail2()
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("iwhat02018@gmail.com");

            mail.From = new MailAddress("thomasjoinau@gmail.com");
            mail.Subject = "Email using Gmail";

            string Body = "Hi, this mail is to test sending mail" +
                          "using Gmail in ASP.NET";
            mail.Body = Body;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential
                 ("iwhat02018@gmail.com", "ImmoWhat2018");

            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(mail);

        }

        public ActionResult VoirMesMails(int idMembre)
        {
            try
            {
                Models.RequestResultM resultRequest = BLL.MembreBLL.GetListOfMails(idMembre);
                List<Models.Mail> listeMails = resultRequest.listeMails;

                return View(listeMails);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    
}
}