using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class RechercheMembreModels
    {
        [Display(ResourceType = typeof(Resource), Name = "id")]
        public int idMembre { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "nom")]
        public string nom { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "prenom")]
        public string prenom { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "mail")]
        public string email { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "dateEnregistrement")]
        public DateTime dateEnregistrement { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "derniereConnection")]
        public Nullable<DateTime> dateDerniereConnection { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "deleted")]
        public bool deleted { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "administrator")]
        public bool administrator { get; set; }
    }
}