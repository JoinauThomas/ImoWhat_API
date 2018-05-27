﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

            List<Models.nbPieces> nbPieces = new List<Models.nbPieces>
            {
                new Models.nbPieces{qtite = 0, affiche = "0"},
                new Models.nbPieces{qtite = 1, affiche = "1"},
                new Models.nbPieces{qtite = 2, affiche = "2"},
                new Models.nbPieces{qtite = 3, affiche = "3"},
                new Models.nbPieces{qtite = 4, affiche = "4"},
                new Models.nbPieces{qtite = 5, affiche = "5"},
                new Models.nbPieces{qtite = 6, affiche = "6"},
                new Models.nbPieces{qtite = 7, affiche = "7"},
                new Models.nbPieces{qtite = 8, affiche = "8"},
                new Models.nbPieces{qtite = 9, affiche = "9"},
                new Models.nbPieces{qtite = 10, affiche = "10+"},
            };

            List<string>noteNRJ = new List<string> { "--","A", "B", "C", "D", "E", "F", "G"};


            ViewBag.nrj = new SelectList(noteNRJ);
            ViewBag.nbPieces = new SelectList(nbPieces, "qtite", "affiche");
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

            List<Models.nbPieces> nbPieces = new List<Models.nbPieces>
            {
                new Models.nbPieces{qtite = 0, affiche = "0"},
                new Models.nbPieces{qtite = 1, affiche = "1"},
                new Models.nbPieces{qtite = 2, affiche = "2"},
                new Models.nbPieces{qtite = 3, affiche = "3"},
                new Models.nbPieces{qtite = 4, affiche = "4"},
                new Models.nbPieces{qtite = 5, affiche = "5"},
                new Models.nbPieces{qtite = 6, affiche = "6"},
                new Models.nbPieces{qtite = 7, affiche = "7"},
                new Models.nbPieces{qtite = 8, affiche = "8"},
                new Models.nbPieces{qtite = 9, affiche = "9"},
                new Models.nbPieces{qtite = 10, affiche = "10+"},
            };

            List<string> noteNRJ = new List<string> { "--", "A", "B", "C", "D", "E", "F", "G" };

            ViewBag.nrj = new SelectList(noteNRJ);
            ViewBag.nbPieces = new SelectList(nbPieces, "qtite", "affiche");
            ViewBag.ListeTypesBiens = new SelectList(typesBiensList, "idTypeBien", "libelle");

            if (!ModelState.IsValid)
            {
               

                return View(monNvBien);
            }
            Models.MembreModels monProfile = (Models.MembreModels)Session["moi"];
            monNvBien.idProprietaire = monProfile.idMembre;

            Models.RequestResultM resultRequest = BLL.BienBLL.addNewBien(monNvBien);

            if(resultRequest.result == "OK")
            {
                return Json(new { success = true, responseText = resultRequest.msg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = resultRequest.msg }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddPhotoBien (List<string> mesfichiers)
        {
            for(int i = 0; i<mesfichiers.Count; i++)
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(mesfichiers[i])))
                {
                    using (Bitmap bm2 = new Bitmap(ms))
                    {
                        bm2.Save("C:/Users/thoma/OneDrive/EPHEC/TFE/immowhat-API/ImmoWhatApp/ImmoWhatApp/img/bien/" + "ImageName"+i+".jpg");
                    }
                }
            }
           
        

           

            return View();
        }
    }
}