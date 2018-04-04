using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class BienModels
    {
        public int idBien { get; set; }
        public int typeBien { get; set; }
        public int idProprietaire { get; set; }
        public int prix { get; set; }
        public int superficie { get; set; }
        public string commune { get; set; }
        public string rue { get; set; }
        public string numero { get; set; }
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