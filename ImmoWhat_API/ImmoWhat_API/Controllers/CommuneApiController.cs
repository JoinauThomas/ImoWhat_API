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
        [Route("GetAveragePrice")]
        public int GetAveragePrice(int annee, string typeBien, string codePostale)
        {
            DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
            List<DAL.GetAveragePrice1_Result> result = dbContext.GetAveragePrice1(2014, "maison", "1410").ToList();

            return (int)result[0].prixMoyen;
        }
        [HttpGet]
        [Route("GetAverageClass")]
        public int GetAverageClass(int annee, string typeBien, string codePostale)
        {
            int classe;
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ImmoWhat;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("GetClassePrice", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@annee", SqlDbType.Int).Value = annee;
                        cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = typeBien;
                        cmd.Parameters.Add("@codePostCommune", SqlDbType.NVarChar).Value = codePostale;

                        con.Open();
                        var x = cmd.ExecuteScalar();
                        classe = (int)x;

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

    }
    
}
