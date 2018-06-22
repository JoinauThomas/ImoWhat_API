using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class RechercheBienModels
    {
        public int idBien { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string commune { get; set; }
        public string codePostal { get; set; }
        public string rue { get; set; }
        public string numero { get; set; }
        public string boite { get; set; }
        public int prix { get; set; }
        public bool deleted { get; set; }
        public bool vendu { get; set; }
    }
}