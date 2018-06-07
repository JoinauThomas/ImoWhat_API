using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public JsonResult changeStatutMailToLu(int idMail)
        {
            Models.RequestResultM requete = BLL.MailBLL.changeStatutMailToLu(idMail);

            if(requete.result == "OK")
                return Json(new { result = "OK", message = requete.msg }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { result = "NoOK", message = requete.msg }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VoirMesMails(int idMembre)
        {
            try
            {
                Models.RequestResultM resultRequest = BLL.MembreBLL.GetListOfMails(idMembre);
                List<Models.Mail> listeMails = resultRequest.listeMails;

                return View(listeMails);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}