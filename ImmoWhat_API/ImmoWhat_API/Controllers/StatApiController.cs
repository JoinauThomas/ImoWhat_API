using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmoWhat_API.Controllers
{
    [RoutePrefix("api/StatApi")]
    public class StatApiController : ApiController
    {
        [HttpGet]
        [Route("GetTableGraphique")]
        public List<Models.TableResultGraphic> GetTableGraphique(string codePostal)
        {
            List<Models.TableResultGraphic> resultGraph = new List<Models.TableResultGraphic>();
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetTableGraphique_Result> ListeStatDB = dbContext.GetTableGraphique(codePostal).ToList();

                foreach (var x in ListeStatDB)
                {
                    resultGraph.Add(new Models.TableResultGraphic { annee = (int)x.annee, moyennePrixAppartement = (int)x.prixMoyenAppartement, moyennePrixMaison = (int)x.prixMoyenMaison, moyennePrixTerrain = (int)x.prixMoyenTerrain, moyennePrixVilla = (int)x.prixMoyenVilla});
                }

                return resultGraph;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
