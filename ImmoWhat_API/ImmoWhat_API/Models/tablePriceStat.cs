using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class tablePriceStat
    {
        public string type { get; set; }
        public int anneeRecherche { get; set; }
        public int anneePrecedente { get; set; }
        public int anneeCourante { get; set; }
        public float evolutionPrice { get; set; }
    }
}