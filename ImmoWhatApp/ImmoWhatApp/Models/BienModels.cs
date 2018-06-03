using System;
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

        [Display(ResourceType = typeof(Resource), Name = "prix")]
        [Required(ErrorMessage = "Le nom du restaurant doit être saisi")]
        public int prix { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "superficie")]
        [Required(ErrorMessage = "Le nom du restaurant doit être saisi")]
        public int superficie { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "commune")]
        [Required(ErrorMessage = "Le nom du restaurant doit être saisi")]
        public string commune { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "codePostale")]
        [Required(ErrorMessage = "Le nom du restaurant doit être saisi")]
        public string codePostale { get; set; }

        [Required(ErrorMessage = "Le nom du restaurant doit être saisi")]
        [Display(ResourceType = typeof(Resource), Name = "rue")]
        public string rue { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "numero")]
        public string numero { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "boite")]
        public string boite { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbEtages")]
        public int nbEtages { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "libelleDescription")]
        public string libelle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "anneeDeConstruction")]
        public int anneeConstruction { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "energie")]
        public string energie { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "supprime")]
        public bool estSupprime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "vendu")]
        public bool estVendu { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbPhotos")]
        public int nbPhotos { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbChambres")]
        public int nbChambres { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbSdb")]
        public int nbSdb { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbDressing")]
        public int nbDressing { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbSam")]
        public int nbSam { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbSalon")]
        public int nbSalon { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbBuanderie")]
        public int nbBuanderie { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbCave")]
        public int nbCave { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbGrenier")]
        public int nbGrenier { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbToilette")]
        public int nbToilette { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbVeranda")]
        public int nbVeranda { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nbGarage")]
        public int nbGarage { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "piscine")]
        public bool piscine { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "cuisineEquipee")]
        public bool cuisineEquipee { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "parking")]
        public bool parking { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "jardin")]
        public bool jardin { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "alarme")]
        public bool alarme { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "doubleVitrage")]
        public bool doubleVitrage { get; set; }
    }
}