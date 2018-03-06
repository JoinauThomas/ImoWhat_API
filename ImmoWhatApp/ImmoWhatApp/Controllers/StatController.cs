using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.Controllers
{
    public class StatController : Controller
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
        public ActionResult GetAverageAndTransactionsTable(int anneeRecherchee, string codePostal)
        {
            try
            {
                List<Models.tablePriceStat> result = BLL.StatBLL.GetPriceTable(anneeRecherchee, codePostal).ToList();
                return PartialView("_tableView",result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}