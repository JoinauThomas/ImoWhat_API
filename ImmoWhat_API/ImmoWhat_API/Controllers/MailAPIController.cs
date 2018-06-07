using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmoWhat_API.Controllers
{
    [RoutePrefix("api/MailAPI")]
    public class MailAPIController : ApiController
    {
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
    }
}
