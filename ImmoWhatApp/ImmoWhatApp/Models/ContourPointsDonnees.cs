using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class ContourPointsDonnees
    {
        public string commune { get; set; }
        public string codePostal { get; set; }
        public string couleur { get; set; }
        public Models.coordonnees coordCentr { get; set; }
        public List<Models.coordonnees> coordPts { get; set; }
    }
}