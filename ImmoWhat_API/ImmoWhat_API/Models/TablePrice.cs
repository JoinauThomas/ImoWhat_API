using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class TablePrice
    {
        public int idClasse { get; set; }
        public int valMin { get; set; }
        public int valMax { get; set; }
        public string refColor { get; set; }
    }
}