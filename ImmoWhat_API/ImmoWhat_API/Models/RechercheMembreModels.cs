using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class RechercheMembreModels
    {
        public int idMembre { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public DateTime dateEnregistrement { get; set; }
        public Nullable<DateTime> dateDerniereConnection { get; set; }
        public bool deleted { get; set; }
        public bool administrator { get; set; }
    }
}