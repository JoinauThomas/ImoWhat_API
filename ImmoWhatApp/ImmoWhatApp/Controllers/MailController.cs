using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.Controllers
{
    public class MailController : BaseController
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialViewListMails(int idMembre)
        {
            try
            {
                Models.RequestResultM resultRequest = BLL.MailBLL.GetListOfMails(idMembre);
                List<Models.Mail> listeMails = resultRequest.listeMails;

                ViewBag.idMembre = idMembre;

                return PartialView(listeMails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult changeStatutMailToLu(int idMail)
        {
            Models.RequestResultM requete = BLL.MailBLL.changeStatutMailToLu(idMail);

            if(requete.result == "OK")
                return Json(new { result = "OK", message = requete.msg }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { result = "NoOK", message = requete.msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult changeStatutMailToDeleted(int idMail)
        {
            Models.RequestResultM requete = BLL.MailBLL.changeStatutMailToDeleted(idMail);

            if (requete.result == "OK")
                return Json(new { result = "OK", message = requete.msg }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { result = "NoOK", message = requete.msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCountOfNewMails(int idMembre)
        {
            try
            {
                Models.RequestResultM resuleRequest = BLL.MailBLL.GetCountOfNewMails(idMembre);

                if (resuleRequest.result == "OK")
                    return Json(new { result = "OK", nbNvMsg = resuleRequest.resultRequest }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { result = "NoOK", msg = resuleRequest.msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        public JsonResult SendMail(int? idMail, string adresseMail, string msg, int Recepteur, string sujet)
        {
            try
            {
                Models.MembreModels moi = (Models.MembreModels)Session["Moi"];


                Models.Mail newMail = new Models.Mail {idMail = (int)idMail, adresseMail = adresseMail, message = msg, idRecepteur = Recepteur, sujet = sujet, idEmetteur = moi.idMembre };

                Models.RequestResultM resultRequest = BLL.MailBLL.sendMail(newMail);

                if (resultRequest.result == "ok")
                    return Json(new { result = "OK", msg = resultRequest.msg });
                else
                    return Json(new { result = "NoOK", msg = resultRequest.msg });


            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult SendResponseMail(int? idMail, string adresseMail, string msg, int destination, string sujet)
        {
            try
            {
                Models.MembreModels moi = (Models.MembreModels)Session["Moi"];


                Models.Mail newMail = new Models.Mail { adresseMail = adresseMail, message = msg, idRecepteur = destination, sujet = sujet, idEmetteur = moi.idMembre, idMail = (int)idMail };

                Models.RequestResultM resultRequest = BLL.MailBLL.sendMail(newMail);

                if (resultRequest.result == "ok")
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
                Models.RequestResultM resultRequest = BLL.MailBLL.GetListOfMails(idMembre);
                List<Models.Mail> listeMails = resultRequest.listeMails;

                Models.MembreModels moi = (Models.MembreModels)Session["Moi"];

                ViewBag.idMembre = idMembre;
                ViewBag.mail = moi.mail;

                return View(listeMails);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}