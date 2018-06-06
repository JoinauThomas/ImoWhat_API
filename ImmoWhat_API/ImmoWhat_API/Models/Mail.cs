using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class Mail
    {
        public int idMail { get; set; }
        public string adresseMail { get; set; }
        public DateTime dateEnvoi { get; set; }
        public string sujet { get; set; }
        public string message { get; set; }
        public bool lu { get; set; }
        public DateTime repondu { get; set; }
        public int idProprietaire { get; set; }
    }
}