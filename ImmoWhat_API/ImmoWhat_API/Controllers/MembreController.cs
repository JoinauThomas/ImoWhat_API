using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmoWhat_API.Controllers
{
    public class MembreController : ApiController
    {
        [HttpPost]
        public IHttpActionResult addNewMembre(Models.MembreModels newMembre)
        {
            DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
            dbContext.addNewMembre(newMembre.mail, newMembre.nom, newMembre.prenom, newMembre.Commune, newMembre.rue, newMembre.numero, newMembre.boite, newMembre.dateDeNaissance, newMembre.telephone);

            return Ok();
        }
    }
}
