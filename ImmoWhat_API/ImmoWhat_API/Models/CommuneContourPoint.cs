using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class CommuneContourPoint
    {
        public int typeBien { get; set; }
        public List<Models.ContourPointsDonnees> donnees { get; set; }
    }
}