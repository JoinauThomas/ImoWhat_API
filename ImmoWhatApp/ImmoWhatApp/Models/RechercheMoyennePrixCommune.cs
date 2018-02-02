using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class RechercheMoyennePrixCommune
    {
        public string codePostal { get; set; }
        public string tybeBien { get; set; }
        public int moyennePrix { get; set; }
    }
}