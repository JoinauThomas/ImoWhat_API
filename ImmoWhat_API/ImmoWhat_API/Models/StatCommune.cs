using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class StatCommune
    {
        public int nbHab { get; set; }
        public int nbFemmes { get; set; }
        public int nbHommes { get; set; }
        public int NbHommesCelib { get; set; }
        public int NbFemmesCelib { get; set; }
        public int nbHommesAge1 { get; set; }
        public int nbFemmesAge1 { get; set; }
        public int nbHommesAge2 { get; set; }
        public int nbFemmesAge2 { get; set; }
        public int nbHommesAge3 { get; set; }
        public int nbFemmesAge3 { get; set; }
        public int nbHommesAge4 { get; set; }
        public int nbFemmesAge4 { get; set; }
        public int nbHommesAge5 { get; set; }
        public int nbFemmesAge5 { get; set; }
        public int nbHommesAge6 { get; set; }
        public int nbFemmesAge6 { get; set; }
        public int nbHommesAge7 { get; set; }
        public int nbFemmesAge7 { get; set; }
        public int nbHommesAge8 { get; set; }
        public int nbFemmesAge8 { get; set; }
        public int nbHommesAge9 { get; set; }
        public int nbFemmesAge9 { get; set; }
        public int nbHommesAge10 { get; set; }
        public int nbFemmesAge10 { get; set; }
        public List<Models.PrenomsPopulaires> listePrenoms { get; set; }
    }
}