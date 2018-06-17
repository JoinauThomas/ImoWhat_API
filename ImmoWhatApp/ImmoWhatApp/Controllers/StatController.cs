using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.Controllers
{
    public class StatController : BaseController
    {
        
        [HttpGet]
        public JsonResult GetJsonResultGraph(string codePostal)
        {
            List<Models.TableResultGraphic> result = BLL.StatBLL.GetTableGraphique(codePostal).ToList();
            if (result != null)
            {
                return Json(new { result = "OK", resultat = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }


        [HttpGet]
        public JsonResult GetTableGraphiqueTransactionInJson(string codePostal)
        {
            List<Models.TableGraphTransactionsModels> result = BLL.StatBLL.GetTableGraphiqueTransaction(codePostal).ToList();
            if (result != null)
            {
                return Json(new { result = "OK", resultat = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public JsonResult GetJsonTablePriceStat(int anneeRecherchee, string codePostal)
        {
            List<Models.tablePriceStat> result = BLL.StatBLL.GetPriceTable(anneeRecherchee, codePostal).ToList();
            if (result != null)
            {
                return Json(new { result = "OK", resultat = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }
        
        [HttpGet]
        public JsonResult GetAverageAndTransactionsTableInJson(int anneeRecherchee, string codePostal)
        {
            try
            {
                int anneeMin = BLL.HomeBLL.GetMinYear(codePostal);
                Session["anneeMin"] = anneeMin;

                List<Models.tablePriceStat> resultTable = BLL.StatBLL.GetAverageAndTransactionsTable(anneeRecherchee, codePostal).ToList();
                if (resultTable != null)
                {
                    return Json(new { result = "OK", resultat = resultTable }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        [HttpGet]
        public ActionResult PartialTableStat(int anneeRecherchee, string codePostal)
        {
            try
            {
                int anneeMin = BLL.HomeBLL.GetMinYear(codePostal);
                Session["anneeMin"] = anneeMin;
                

                int anneeCourante = (int)Session["HighestYear"];
                int anneePrec = anneeCourante - 1;
                int anneeRecherch = anneeRecherchee;
                
                ViewBag.anneeCour = anneeCourante;
                ViewBag.anneePrec = anneePrec;
                ViewBag.anneeRech = anneeRecherch;

                List <Models.tablePriceStat> resultTable = BLL.StatBLL.GetAverageAndTransactionsTable(anneeRecherchee, codePostal).ToList();
                if (resultTable != null)
                {
                    return PartialView(resultTable);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpGet]
        public JsonResult GetTableTransactionsInJson(int anneeRecherchee, string codePostal)
        {
            try
            {
                List<Models.tablePriceStat> resultTable = BLL.StatBLL.GetTableTransactions(anneeRecherchee, codePostal).ToList();
                if (resultTable != null)
                {
                    return Json(new { result = "OK", resultat = resultTable }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult PartialViewPoiCommune(string latitude, string longitude)
        {

            ViewBag.latitude = latitude;
            ViewBag.longitude = longitude;
            return PartialView();
               
        }

        [HttpGet]
        public ActionResult PartialViewPoiAdresse(string latitude, string longitude)
        {

            ViewBag.latitude = latitude;
            ViewBag.longitude = longitude;
            return PartialView();

        }

        [HttpGet]
        public ActionResult PartialViewInfoCommune(string commune)
        {
            int age = -1;

            Models.MembreModels moi = (Models.MembreModels)Session["moi"];
            if(moi != null)
            {
                DateTime Naissance = moi.dateDeNaissance;
                age = DateTime.Now.Year - Naissance.Year - (DateTime.Now.Month < Naissance.Month ? 1 : (DateTime.Now.Month == Naissance.Month && DateTime.Now.Day < Naissance.Day) ? 1 : 0);
            }
            

            Models.StatCommune statCommune = BLL.CommuneBLL.GetInfosCommune(commune, age);
            ViewBag.commune = commune;

            return PartialView(statCommune);

        }
    }
}