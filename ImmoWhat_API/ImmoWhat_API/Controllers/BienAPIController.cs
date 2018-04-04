using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmoWhat_API.Controllers
{
    [RoutePrefix("api/BienAPI")]
    public class BienAPIController : ApiController
    {
        [HttpPost]
        [Route("addNewBien")]
        public IHttpActionResult addNewBien(Models.BienModels newBien)
        {
           

            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                dbContext.addNewBien(newBien.typeBien, newBien.idProprietaire, newBien.prix, newBien.superficie, newBien.commune, newBien.rue, newBien.numero, newBien.boite, newBien.nbEtages, newBien.libelle, newBien.anneeConstruction, newBien.energie, newBien.nbPhotos, newBien.nbChambres, newBien.nbSdb, newBien.nbDressing, newBien.nbSam, newBien.nbSalon, newBien.nbBuanderie, newBien.nbCave, newBien.nbGrenier, newBien.nbToilette, newBien.nbVeranda, newBien.nbGarage, newBien.piscine, newBien.cuisineEquipee, newBien.parking, newBien.jardin, newBien.alarme, newBien.doubleVitrage);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
