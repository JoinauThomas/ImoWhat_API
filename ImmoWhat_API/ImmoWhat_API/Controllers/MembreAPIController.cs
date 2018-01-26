using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmoWhat_API.Controllers
{
    [RoutePrefix("api/MembreAPI")]
    public class MembreAPIController : ApiController
    {
        [HttpPost]
        [Route("addNewMembre")]
        public IHttpActionResult addNewMembre(Models.MembreModels newMembre)
        {
            Models.ResultInscription resultInscription = new Models.ResultInscription();

            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                dbContext.addNewMembre(newMembre.mail, newMembre.nom, newMembre.prenom, newMembre.Commune, newMembre.rue, newMembre.numero, newMembre.boite, newMembre.dateDeNaissance, newMembre.telephone);
            }
            catch
            {

            }
           

            return Ok();
        }

        [HttpGet]
        [Route("GetMyId")]
        public int GetMyId(string mail)
        {

            // récuperation de l'id du nveau membre

            DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
            List<DAL.MEMBRE> Membres = new List<DAL.MEMBRE>();
            Membres = dbContext.MEMBRE.ToList();
            DAL.MEMBRE moi = Membres.Find(x => (x.mail == mail));
            return moi.idMembre;
        }

        [HttpGet]
        [Route("GetMyProfile")]
        public Models.MembreModels GetMyProfile(string mail)
        {
            DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();

            List<DAL.GetMyProfile_Result> moiDB = dbContext.GetMyProfile(mail).ToList();


            Models.MembreModels moi = new Models.MembreModels { idMembre = moiDB[0].idMembre, nom = moiDB[0].nom, prenom = moiDB[0].prenom
                , dateDeNaissance = moiDB[0].dateDeNaissance, Commune = moiDB[0].Commune, rue = moiDB[0].rue, numero = moiDB[0].numero, boite = moiDB[0].boite,
                mail = moiDB[0].mail, roleUser = moiDB[0].Role, telephone = moiDB[0].PhoneNumber, photo = moiDB[0].photo
            };
            return moi;
        }
    }
}
