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
                dbContext.addNewMembre(newMembre.mail, newMembre.nom, newMembre.prenom, newMembre.Commune, newMembre.rue, newMembre.numero, newMembre.boite, newMembre.dateDeNaissance, newMembre.telephone, newMembre.photo);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetMyId")]
        public int GetMyId(string mail)
        {
            try
            {
                // récuperation de l'id du nveau membre

                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                List<DAL.MEMBRE> Membres = new List<DAL.MEMBRE>();
                Membres = dbContext.MEMBRE.ToList();
                DAL.MEMBRE moi = Membres.Find(x => (x.mail == mail));
                return moi.idMembre;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Authorize]
        [Route("GetMyProfile")]
        public Models.MembreModels GetMyProfile(string mail)
        {
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();

                List<DAL.GetMyProfiles_Result> moiDB = dbContext.GetMyProfiles(mail).ToList();


                Models.MembreModels moi = new Models.MembreModels
                {
                    idMembre = moiDB[0].idMembre,
                    nom = moiDB[0].nom,
                    prenom = moiDB[0].prenom
                    ,
                    dateDeNaissance = moiDB[0].dateDeNaissance,
                    Commune = moiDB[0].Commune,
                    rue = moiDB[0].rue,
                    numero = moiDB[0].numero,
                    boite = moiDB[0].boite,
                    mail = moiDB[0].mail,
                    roleUser = moiDB[0].RoleMembre,
                    telephone = moiDB[0].PhoneNumber,
                    photo = moiDB[0].photo,
                    dateEnregistrement = moiDB[0].DateEnregistrement,
                    deleted = moiDB[0].deleted,
                    estProprietaire = moiDB[0].estProprietaire
                };
                return moi;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpGet]
        [Route("CheckIfMailExists")]
        public bool CheckIfMailExists(string mail)
        {
            bool x;
            try
            {
                DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ImmoWhat;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("CheckIfMailExists", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@mail", SqlDbType.VarChar).Value = mail;

                            con.Open();
                            var y = cmd.ExecuteScalar().ToString();
                            if (y == "0")
                                x = false;
                            else
                                x = true;
                            con.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

                List<DAL.GetListOfMails_Result> listeMailsResult = dbContext.GetListOfMails(idMembre).ToList();

                List<Models.Mail> listeMail = new List<Models.Mail>();

                foreach(var x in listeMailsResult)
                {
                    if(x.repondu != null)
                        listeMail.Add(new Models.Mail { adresseMail = x.adresseMail, idProprietaire = x.idProprietaire, dateEnvoi = x.dateEnvoi, idMail = x.idMail, lu = x.lu, message = x.msg, repondu = (DateTime)x.repondu, sujet = x.sujet });
                    else
                        listeMail.Add(new Models.Mail { adresseMail = x.adresseMail, idProprietaire = x.idProprietaire, dateEnvoi = x.dateEnvoi, idMail = x.idMail, lu = x.lu, message = x.msg, sujet = x.sujet });

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
                dbContext.SendMail(newMail.adresseMail, newMail.sujet, newMail.message, newMail.idProprietaire);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost]
        //[Route("changeStatutMailToLu")]
        //public IHttpActionResult changeStatutMailToLu(int idMail)
        //{
        //    try
        //    {
        //        DAL.ImmoWhatEntities dbContext = new DAL.ImmoWhatEntities();
        //        dbContext.changeStatutMailToLu(idMail);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
