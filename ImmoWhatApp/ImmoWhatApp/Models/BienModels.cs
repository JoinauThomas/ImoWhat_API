﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class BienModels
    {

        public int idBien { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "typeBien")]
        public int typeBien { get; set; }


        public int idProprietaire { get; set; }
        public int prix { get; set; }
        public int superficie { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "commune")]
        public string commune { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "codePostale")]
        public string codePostal { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "rue")]
        public string rue { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "numero")]
        public string numero { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "boite")]
        public string boite { get; set; }
        public int nbEtages { get; set; }
        public string libelle { get; set; }
        public int anneeConstruction { get; set; }
        public string energie { get; set; }
        public bool estSupprime { get; set; }
        public bool estVendu { get; set; }
        public int nbPhotos { get; set; }
        public int nbChambres { get; set; }
        public int nbSdb { get; set; }
        public int nbDressing { get; set; }
        public int nbSam { get; set; }
        public int nbSalon { get; set; }
        public int nbBuanderie { get; set; }
        public int nbCave { get; set; }
        public int nbGrenier { get; set; }
        public int nbToilette { get; set; }
        public int nbVeranda { get; set; }
        public int nbGarage { get; set; }
        public bool piscine { get; set; }
        public bool cuisineEquipee { get; set; }
        public bool parking { get; set; }
        public bool jardin { get; set; }
        public bool alarme { get; set; }
        public bool doubleVitrage { get; set; }
    }
}