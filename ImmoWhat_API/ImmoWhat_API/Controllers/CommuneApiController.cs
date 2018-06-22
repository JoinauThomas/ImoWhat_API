using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmoWhat_API.Controllers
{
    [RoutePrefix("api/CommuneApi")]
    public class CommuneApiController : ApiController
    {

        [HttpGet]
        public List<Models.CommuneComplet> GetCommunesComplet()
        {
            List<Models.CommuneComplet> MaListe = new List<Models.CommuneComplet>();

            DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
            List<DAL.COMMUNE> LaListe = new List<DAL.COMMUNE>();
            LaListe = dbContext.COMMUNE.ToList();

            foreach(var i in LaListe)
            {
                MaListe.Add(new Models.CommuneComplet { CodePostal = i.CodePostal, id = i.idCommune, langue = i.langue, Localite = i.Localite, Province = i.Province, latitude = i.latitude, longitude = i.longitude });
            }

            return MaListe;
        }

        [HttpGet]
        [Route("GetACommuneWithCodePostal")]
        public Models.CommuneComplet GetACommuneWithCodePostal(string codePostal, string langue)
        {
            
            DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
            List<DAL.GetACommuneWithCodePostal_Result> result = dbContext.GetACommuneWithCodePostal(codePostal, langue).ToList();

            Models.CommuneComplet maCommune = new Models.CommuneComplet { id = result[0].idCommune, CodePostal = result[0].CodePostal, langue = result[0].langue, latitude = result[0].latitude, Localite = result[0].Localite, longitude = result[0].longitude, Province = result[0].Province };

            return maCommune;
        }


        [HttpGet]
        [Route("GetAveragePrice")]
        public int GetAveragePrice(int annee, string typeBien, string codePostale)
        {
            DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
            List<DAL.GetAveragePrice1_Result> result = dbContext.GetAveragePrice1(annee, typeBien, codePostale).ToList();
            int res = 0;

            if (result[0].prixMoyen == null)
            {
                res = 180000;
            }
            else
            {
                res = (int)result[0].prixMoyen;
            }
                
            return res;
        }

        [HttpGet]
        [Route("GetInfoMainMenu")]
        public int GetInfoMainMenu(int annee, string typeBien, string codePostale)
        {
            int classe;
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ImmoWhat;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("GetInfoMainMenu", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@annee", SqlDbType.Int).Value = annee;
                        cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = typeBien;
                        cmd.Parameters.Add("@codePostCommune", SqlDbType.NVarChar).Value = codePostale;

                        con.Open();
                        var x = cmd.ExecuteScalar();
                        if (x == null)
                        {
                            classe = 180000;
                        }
                        else
                        {
                            classe = (int)x;
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return classe;
        }

        [HttpGet]
        [Route("GetTableClassPrice")]
        public List<Models.TablePrice> GetTableClassPrice(string typeBien)
        {
            List<Models.TablePrice> tablePrix = new List<Models.TablePrice>();
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ImmoWhat;Integrated Security=True"))
                {
                    
                    using (SqlCommand cmd = new SqlCommand("GetTableClassPrice", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = typeBien;

                        con.Open();
                        var x = cmd.ExecuteReader();
                        while(x.Read())
                        {
                            tablePrix.Add(new Models.TablePrice { idClasse = (int)x["idClasse"], valMax = (int)x["valMax"], valMin = (int)x["valMin"], refColor = x["refColor"].ToString() });
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return tablePrix;
        }

        [HttpGet]
        [Route("GetInfoEvolutionPrices")]
        public List<Models.TableStatModels> GetInfoEvolutionPrices (string codePostal)
        {
            List<Models.TableStatModels> mesStats = new List<Models.TableStatModels>();
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetInfoEvolutionPrices_Result> ListeStatDB = dbContext.GetInfoEvolutionPrices(codePostal).ToList();

                foreach(var x in ListeStatDB)
                {
                    mesStats.Add(new Models.TableStatModels { annee = (int)x.annee, moyenne = (int)x.moyenne, nbTransaction = (int)x.nbTransaction, typeBien = x.typeBien });
                }

                return mesStats;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetCommuneContourPoints")]
        public Models.CommuneContourPoint GetCommuneContourPoints( int idType)
        {
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetJsonCoordForType_Result> Result = dbContext.GetJsonCoordForType(idType).ToList();
                string CP = "";
                string commune = "";
                string couleur = "";
                Models.coordonnees coordCentre = new Models.coordonnees();
                List<Models.coordonnees> listePoints = new List<Models.coordonnees>();
                List<Models.ContourPointsDonnees> donnees = new List<Models.ContourPointsDonnees>();
                

                foreach (var x in Result)
                {
                        
                    if(x.codePostal != CP)
                    {
                        if(CP!="")
                        {
                            donnees.Add(new Models.ContourPointsDonnees { codePostal = CP, commune = commune, coordCentr = coordCentre, couleur = couleur, coordPts = listePoints });
                        }
                        CP = x.codePostal;
                        couleur = x.couleur;
                        commune = x.commune;
                        coordCentre = new Models.coordonnees { lat = double.Parse(x.latCentre), lng = double.Parse(x.lngCentre) };
                        listePoints = new List<Models.coordonnees>();
                        //listePoints.Clear();
                    }
                    listePoints.Add(new Models.coordonnees { id= (int)x.idCoord, lat = double.Parse(x.latPt), lng = double.Parse(x.lngPt) });
                }
                donnees.Add(new Models.ContourPointsDonnees { codePostal = CP, commune = commune, coordCentr = coordCentre, couleur = couleur, coordPts = listePoints });

                Models.CommuneContourPoint contourPoints = new Models.CommuneContourPoint {donnees = donnees, typeBien = idType };

                return contourPoints;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetInfosCommune")]
        public Models.StatCommune GetInfosCommune(string commune, int age)
        {
            
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetPrenomsPopulaires_Result> LstPrenomsTemp = dbContext.GetPrenomsPopulaires(commune).ToList();

                List<DAL.GetStatCommune2_Result> statCommuneTemp = dbContext.GetStatCommune2(commune, age).ToList();

                List<Models.PrenomsPopulaires> listePrenoms = new List<Models.PrenomsPopulaires>();
                foreach (var x in LstPrenomsTemp)
                {
                    listePrenoms.Add(new Models.PrenomsPopulaires { prenom = x.prenom, nombreDePersonnes = (int)x.nbPersonnes, sexe = x.sexe });
                }

                Models.StatCommune statCommune = new Models.StatCommune
                {
                    nbHab = (int)statCommuneTemp[0].nbHab,
                    nbFemmes = (int)statCommuneTemp[0].nbFemmes,
                    nbHommes = (int)statCommuneTemp[0].nbHommes,
                    nbHommesAge1 = (int)statCommuneTemp[0].nbHommesAge1,
                    nbFemmesAge1 = (int)statCommuneTemp[0].nbFemmesAge1,
                    nbHommesAge2 = (int)statCommuneTemp[0].nbHommesAge2,
                    nbFemmesAge2 = (int)statCommuneTemp[0].nbFemmesAge2,
                    nbHommesAge3 = (int)statCommuneTemp[0].nbHommesAge3,
                    nbFemmesAge3 = (int)statCommuneTemp[0].nbFemmesAge3,
                    nbHommesAge4 = (int)statCommuneTemp[0].nbHommesAge4,
                    nbFemmesAge4 = (int)statCommuneTemp[0].nbFemmesAge4,
                    nbHommesAge5 = (int)statCommuneTemp[0].nbHommesAge5,
                    nbFemmesAge5 = (int)statCommuneTemp[0].nbFemmesAge5,
                    nbHommesAge6 = (int)statCommuneTemp[0].nbHommesAge6,
                    nbFemmesAge6 = (int)statCommuneTemp[0].nbFemmesAge6,
                    nbHommesAge7 = (int)statCommuneTemp[0].nbHommesAge7,
                    nbFemmesAge7 = (int)statCommuneTemp[0].nbFemmesAge7,
                    nbHommesAge8 = (int)statCommuneTemp[0].nbHommesAge8,
                    nbFemmesAge8 = (int)statCommuneTemp[0].nbFemmesAge8,
                    nbHommesAge9 = (int)statCommuneTemp[0].nbHommesAge9,
                    nbFemmesAge9 = (int)statCommuneTemp[0].nbFemmesAge9,
                    nbHommesAge10 = (int)statCommuneTemp[0].nbHommesAge10,
                    nbFemmesAge10 = (int)statCommuneTemp[0].nbFemmesAge10,
                    NbFemmesCelib = (int)statCommuneTemp[0].nbFemmeCelibataire,
                    NbHommesCelib = (int)statCommuneTemp[0].hommeCelibataire,
                    listePrenoms = listePrenoms.ToList()
                };

                

                return statCommune;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    
}
