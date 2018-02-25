using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class TableStatModels
    {
        public int annee { get; set; }
        public string typeBien { get; set; }
        public int moyenne { get; set; }
        public int nbTransaction { get; set; }
    }
}