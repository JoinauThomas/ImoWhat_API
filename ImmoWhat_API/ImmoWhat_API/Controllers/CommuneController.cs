using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmoWhat_API.Controllers
{
    public class CommuneController : ApiController
    {

        [HttpGet]
        public List<Models.Commune> mesCommunes()
        {
            List<Models.Commune> MaListe = new List<Models.Commune>();

            DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
            List<DAL.COMMUNES> LaListe = new List<DAL.COMMUNES>();
            LaListe = dbContext.COMMUNES.ToList();

            foreach(var i in LaListe)
            {
                MaListe.Add(new Models.Commune { CodePostal = i.CodePostal, id = i.id, langue = i.langue, Localite = i.Localité, Province = i.Province });
            }

            return MaListe;
        }

        
    }
    
}
