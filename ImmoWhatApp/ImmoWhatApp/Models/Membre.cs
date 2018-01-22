using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class Membre
    {
        public int idMembre { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string password2 { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public int idCommune { get; set; }
        public string rue { get; set; }
        public string numero { get; set; }
        public string boite { get; set; }
        public string role { get; set; }
        public string telephone { get; set; }



    }
}