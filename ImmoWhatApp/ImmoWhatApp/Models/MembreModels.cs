using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class MembreModels
    {
        public int idMembre { get; set; }
        [Required]
        public string mail { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "minimum 6 caractères !")]
        public string pswd { get; set; }
        [Required]
        [Compare("pswd", ErrorMessage = "Les deux mots de pass ne correspondent pas !")]
        public string pswd2 { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public string prenom { get; set; }
        [Required]
        public string Commune { get; set; }
        [Required]
        public string rue { get; set; }
        [Required]
        public string numero { get; set; }

        public string boite { get; set; }

        public string roleUser { get; set; }
       
        public string telephone { get; set; }
        [Required]
        public DateTime dateDeNaissance { get; set; }

        public bool estProprietaire { get; set; }

        public string photo { get; set; }

        public bool deleted { get; set; }
    }
}