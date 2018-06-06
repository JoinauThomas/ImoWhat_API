using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class RequestResultM
    {
        public string result { get; set; }
        public string msg { get; set; }
        public int resultRequest { get; set; }
        public List<Models.Mail> listeMails { get; set; }
    }
}