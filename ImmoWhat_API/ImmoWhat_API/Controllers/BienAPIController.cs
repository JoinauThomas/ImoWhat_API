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

        [HttpGet]
        [Route("GetListBiensFromCPAndType")]
        public List<Models.BienModels> GetListBiensFromCPAndType(string codePostale, int type)
        {
            try
            {
                List<Models.BienModels> listeDeBiens = new List<Models.BienModels>();

                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetListBiensFromCPostAndType_Result> result = dbContext.GetListBiensFromCPostAndType(codePostale, type).ToList();

                foreach (var x in result)
                {
                    listeDeBiens.Add(new Models.BienModels { idBien = x.BIEN_Id, prix = x.BIEN_Prix, superficie = x.BIEN_Superficie, nbChambres = x.nombreChambres });
                }

                return listeDeBiens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetOneBien")]
        public Models.BienModels GetOneBien(int idBien)
        {
            try
            {
                Models.BienModels TheBien = new Models.BienModels();

                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetOneBien_Result> result = dbContext.GetOneBien(idBien).ToList();

                foreach (var x in result)
                {
                    TheBien = new Models.BienModels { idBien = (int)x.idBien, typeBien = (int)x.typeBien, idProprietaire = (int)x.idProprietaire, prix = (int)x.prix, superficie = (int)x.superficie, commune = x.commune, CodePostale = x.codePostal, rue = x.rue, numero = x.numero, boite = x.boite, nbEtages = (int)x.nbEtages, libelle = x.libbelle, anneeConstruction = (int)x.anneeDeConstr, energie = x.energie, estSupprime = (bool)x.estSupprime, estVendu = (bool)x.estVendu, nbPhotos = (int)x.nbPhotos, nbChambres = (int)x.nbChambre, nbSdb = (int)x.nbSdb, nbDressing = (int)x.nbdressing, nbSam = (int)x.nbsam, nbSalon = (int)x.nbsalon, nbBuanderie = (int)x.nbbuanderie, nbCave = (int)x.nbcave, nbGrenier = (int)x.nbgrenier, nbToilette = (int)x.nbtoilette, nbVeranda = (int)x.nbveranda, nbGarage = (int)x.nbgarage, piscine = (bool)x.piscine, cuisineEquipee = (bool)x.cuisineEqu, parking = (bool)x.parking, jardin = (bool)x.jardin, alarme = (bool)x.alarme, doubleVitrage = (bool)x.doubleVitre };
                }

                return TheBien;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetMyBiensList")]
        public List<Models.BienModels> GetMyBiensList(int idMembre)
        {
            try
            {
                List<Models.BienModels> myBiens = new List<Models.BienModels>();

                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetMyBiensList2_Result> result = dbContext.GetMyBiensList2(idMembre).ToList();

                foreach (var x in result)
                {
                    myBiens.Add( new Models.BienModels { idBien = (int)x.BIEN_Id, typeBien = (int)x.BIEN_IdType, idProprietaire = (int)x.BIEN_IdProprietaire, prix = (int)x.BIEN_Prix, superficie = (int)x.BIEN_Superficie, commune = x.commune, CodePostale = x.CodePostal, rue = x.Bien_Rue, numero = x.BIEN_Numero, boite = x.BIEN_Boite, anneeConstruction = (int)x.BIEN_AnneeDeConstruction, energie = x.BIEN_Energie, estSupprime = (bool)x.BIEN_Supprime, estVendu = (bool)x.BIEN_Vendu });
                }

                return myBiens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Authorize]
        [Route("DeleteBienByUser")]
        public IHttpActionResult DeleteBienByUser(Models.BienModels bienASupprimer)
        {
            int idBien = bienASupprimer.idBien;
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                dbContext.DeleteBienByUser(idBien);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Authorize]
        [Route("DeclareBienAsVendu")]
        public IHttpActionResult DeclareBienAsVendu(Models.BienModels bienASupprimer)
        {
            int idBien = bienASupprimer.idBien;
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                dbContext.DeclareBienAsVendu(idBien);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("SearchBiens")]
        public List<Models.RechercheBienModels> SearchBiens(string recherche)
        {
            try
            {
                if (recherche == null)
                    recherche = "";


                List<Models.RechercheBienModels> lesBiens = new List<Models.RechercheBienModels>();

                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.SearchBiens2_Result> result = dbContext.SearchBiens2(recherche).ToList();

                foreach (var x in result)
                {
                    lesBiens.Add(new Models.RechercheBienModels { idBien = (int)x.idBien, nom = x.nom, prenom = x.prenom, commune = x.commune, codePostal = x.CodePostal, rue = x.rue, numero = x.numero, boite = x.boite, prix = (int)x.prix, deleted = x.deleted, vendu = x.vendu });
                }

                return lesBiens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
