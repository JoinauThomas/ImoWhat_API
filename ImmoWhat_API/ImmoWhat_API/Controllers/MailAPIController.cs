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
    [RoutePrefix("api/MailAPI")]
    public class MailAPIController : ApiController
    {
        [HttpGet]
        [Route("GetCountOfNewMails")]
        public int GetCountOfNewMails(int idMembre)
        {
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();

                using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ImmoWhat;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("GetCountOfNewMails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idProprietaire", SqlDbType.Int).Value = idMembre;

                        con.Open();
                        int nbMsg = (int)cmd.ExecuteScalar();

                        con.Close();

                        return nbMsg;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        [HttpGet]
        [Authorize]
        [Route("GetListOfMails")]
        public List<Models.Mail> GetListOfMails(int idMembre)
        {
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();

                List<DAL.GetListOfMails2_Result> listeMailsResult = dbContext.GetListOfMails2(idMembre).ToList();

                List<Models.Mail> listeMail = new List<Models.Mail>();

                foreach (var x in listeMailsResult)
                {
                    if (x.repondu != null)
                        listeMail.Add(new Models.Mail { adresseMail = x.adresseMail, idRecepteur = x.idRecepteur, dateEnvoi = x.dateEnvoi, idMail = x.idMail, lu = x.lu, message = x.msg, repondu = (DateTime)x.repondu, sujet = x.sujet,deleted = x.deleted, idEmetteur = x.idEmetteur });
                    else
                        listeMail.Add(new Models.Mail { adresseMail = x.adresseMail, idRecepteur = x.idRecepteur, dateEnvoi = x.dateEnvoi, idMail = x.idMail, lu = x.lu, message = x.msg, sujet = x.sujet, deleted = x.deleted, idEmetteur = x.idEmetteur });

                }

                return listeMail;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("sendMail")]
        public IHttpActionResult sendMail(Models.Mail newMail)
        {
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                dbContext.SendMail2(newMail.idEmetteur, newMail.adresseMail, newMail.sujet, newMail.message, newMail.idRecepteur);

                if(newMail.idMail != 0)
                {
                    dbContext.changeStatutMailToRepondu(newMail.idMail);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("changeStatutMailToLu")]
        public IHttpActionResult changeStatutMailToLu(Models.Mail monMail)
        {
            int idMail = monMail.idMail;
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                dbContext.changeStatutMailToLu(idMail);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("changeStatutMailToDeleted")]
        public IHttpActionResult changeStatutMailToDeleted(Models.Mail monMail)
        {
            int idMail = monMail.idMail;
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                dbContext.changeStatutMailToDeleted(idMail);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
