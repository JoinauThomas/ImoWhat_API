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
    [RoutePrefix("api/StatApi")]
    public class StatApiController : ApiController
    {
        [HttpGet]
        [Route("GetHighestYear")]
        public int GetHighestYear()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ImmoWhat;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("GetHighestYear", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        var x = cmd.ExecuteScalar();
                        con.Close();

                        return (int)x;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetTableGraphique")]
        public List<Models.TableResultGraphic> GetTableGraphique(string codePostal)
        {
            List<Models.TableResultGraphic> resultGraph = new List<Models.TableResultGraphic>();
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetTableGraphique2_Result> ListeStatDB = dbContext.GetTableGraphique2(codePostal).ToList();

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

        [HttpGet]
        [Route("GetPriceTable")]
        public List<Models.tablePriceStat> GetPriceTable(int anneeRecherchee, string codePostal)
        {
            List<Models.tablePriceStat> resultTable = new List<Models.tablePriceStat>();
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetPriceTable_Result> ListeStatDB = dbContext.GetPriceTable(anneeRecherchee, codePostal).ToList();

                foreach (var x in ListeStatDB)
                {
                    resultTable.Add(new Models.tablePriceStat { type = x.typ, anneeCourante = (int)x.currentyear0, anneePrecedente = (int)x.currentyear1, anneeRecherche = (int)x.anneeRecherchee, evolutionPrice = (int)x.evolutions });
                }

                return resultTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAverageAndTransactionsTable")]
        public List<Models.tablePriceStat> GetAverageAndTransactionsTable(int anneeRecherchee, string codePostal)
        {
            List<Models.tablePriceStat> resultTable = new List<Models.tablePriceStat>();
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.GetAverageAndTransactionsTable_Result> ListeStatDB = dbContext.GetAverageAndTransactionsTable(anneeRecherchee, codePostal).ToList();

                foreach (var x in ListeStatDB)
                {
                    resultTable.Add(new Models.tablePriceStat { type = x.typ, anneeCourante = (int)x.currentyear0, anneePrecedente = (int)x.currentyear1, anneeRecherche = (int)x.anneeRecherchee, evolutionPrice = (int)x.evolutions });
                }

                return resultTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetTableGraphique2")]
        public List<Models.TableResultGraphic> GetTableGraphique2(string codePostal)
        {
            List<Models.TableResultGraphic> liste = new List<Models.TableResultGraphic>();
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ImmoWhat;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTableGraphique", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@codePostal", SqlDbType.VarChar).Value = codePostal;

                        con.Open();
                        var x = cmd.ExecuteScalar();
                        liste = (List<Models.TableResultGraphic>)x;

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return liste;
        }
    }

   
}
