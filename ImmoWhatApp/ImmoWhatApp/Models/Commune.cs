using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class Commune
    {
        public int id { get; set; }
        public string CodePostal { get; set; }
        public string Localite { get; set; }
        public string Province { get; set; }
        public string langue { get; set; }
    }
}