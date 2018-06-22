using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class RechercheBienModels
    {
        [Display(ResourceType = typeof(Resource), Name = "id")]
        public int idBien { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nom")]
        public string nom { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "prenom")]
        public string prenom { get; set; }

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

        [Display(ResourceType = typeof(Resource), Name = "prix")]
        public int prix { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "deleted")]
        public bool deleted { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "vendu")]
        public bool vendu { get; set; }
    }
}