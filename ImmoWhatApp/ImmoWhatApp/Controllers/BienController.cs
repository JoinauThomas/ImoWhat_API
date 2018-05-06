using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.Controllers
{
    public class BienController : BaseController
    {
        // GET: Bien
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddNewBien()
        {
            List<Models.TypeBienModels> typesBiensList = new List<Models.TypeBienModels>
            {
                new Models.TypeBienModels{idTypeBien = 1, libelle = Resource.terrain},
                new Models.TypeBienModels{idTypeBien = 2, libelle = Resource.maison},
                new Models.TypeBienModels{idTypeBien = 3, libelle = Resource.villa},
                new Models.TypeBienModels{idTypeBien = 4, libelle = Resource.appartement}
            };

            ViewBag.ListeTypesBiens = new SelectList(typesBiensList, "idTypeBien", "libelle");
            return View();
        }

        [HttpPost]
        public ActionResult AddNewBien(Models.BienModels monNvBien, List<HttpPostedFileBase> mesfichiers)
        {
            List<Models.TypeBienModels> typesBiensList = new List<Models.TypeBienModels>
            {
                new Models.TypeBienModels{idTypeBien = 1, libelle = Resource.terrain},
                new Models.TypeBienModels{idTypeBien = 2, libelle = Resource.maison},
                new Models.TypeBienModels{idTypeBien = 3, libelle = Resource.villa},
                new Models.TypeBienModels{idTypeBien = 4, libelle = Resource.appartement}
            };

            if (!ModelState.IsValid)
            {
                ViewBag.ListeTypesBiens = new SelectList(typesBiensList, "idTypeBien", "libelle");
                return View(monNvBien);
            }
           

            ViewBag.ListeTypesBiens = new SelectList(typesBiensList, "idTypeBien", "libelle");
            return View(monNvBien);
        }
    }
}