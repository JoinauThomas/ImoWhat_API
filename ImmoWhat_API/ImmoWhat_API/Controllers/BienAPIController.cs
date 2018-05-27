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
        [Authorize]
        [Route("addNewBien")]
        public IHttpActionResult addNewBien(Models.BienModels newBien)
        {
           

            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                dbContext.PostNewBien(newBien.typeBien, newBien.idProprietaire, newBien.prix, newBien.superficie, newBien.commune, newBien.rue, newBien.numero, newBien.boite, newBien.nbEtages, newBien.libelle, newBien.anneeConstruction, newBien.energie, newBien.nbPhotos, newBien.nbChambres, newBien.nbSdb, newBien.nbDressing, newBien.nbSam, newBien.nbSalon, newBien.nbBuanderie, newBien.nbCave, newBien.nbGrenier, newBien.nbToilette, newBien.nbVeranda, newBien.nbGarage, newBien.piscine, newBien.cuisineEquipee, newBien.parking, newBien.jardin, newBien.alarme, newBien.doubleVitrage);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetIdBien")]
        public int GetIdBien(string codePostale, string rue, string numero, string boite)
        {
            int idBien = 0;
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                var result = dbContext.GetIdBien(codePostale, rue, numero, boite);
                foreach(int x in result)
                {
                    idBien = x;
                }
                 
                return idBien;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("CheckIfAdressExists")]
        public bool CheckIfAdressExists(string codePostale, string rue, string numero, string boite)
        {
            bool exist = false;
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                var result = dbContext.CheckIfAdressExists(codePostale, rue, numero, boite);
                foreach (bool x in result)
                {
                    exist = x;
                }

                return exist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Authorize]
        [Route("PostNewPhotos")]
        public IHttpActionResult PostNewPhotos(Models.imageModels images )
        {
            int idBien = images.idBien;
            List<string> liens = images.liens;
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                for(int i = 0; i<liens.Count; i++)
                {
                    dbContext.PostNewPhotos(idBien, liens[i]);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
