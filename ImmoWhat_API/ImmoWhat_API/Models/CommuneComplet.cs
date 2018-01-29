using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class CommuneComplet
    {
        public int id { get; set; }
        public string CodePostal { get; set; }
        public string Localite { get; set; }
        public string Province { get; set; }
        public string langue { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}