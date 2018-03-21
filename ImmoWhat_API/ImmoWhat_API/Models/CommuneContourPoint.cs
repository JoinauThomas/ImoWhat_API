using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class CommuneContourPoint
    {
        public int id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string codePostal { get; set; }
    }
}