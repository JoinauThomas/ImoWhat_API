using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class TableResultGraphic
    {
        public int annee { get; set; }
        public int moyennePrixVilla { get; set; }
        public int moyennePrixMaison { get; set; }
        public int moyennePrixAppartement { get; set; }
        public int moyennePrixTerrain { get; set; }
    }
}