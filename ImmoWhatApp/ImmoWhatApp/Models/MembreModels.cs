using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class MembreModels
    {
        public int idMembre { get; set; }
        public string mail { get; set; }
        public string pswd { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string Commune { get; set; }
        public string rue { get; set; }
        public string numero { get; set; }
        public string boite { get; set; }
        public string roleUser { get; set; }
        public string telephone { get; set; }
        public DateTime dateDeNaissance { get; set; }
        public bool estProprietaire { get; set; }
        public string photo { get; set; }
        public bool deleted { get; set; }
    }
}