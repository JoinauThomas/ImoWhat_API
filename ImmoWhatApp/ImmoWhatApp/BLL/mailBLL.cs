using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace ImmoWhatApp.BLL
{
    public class MailBLL
    {
        public static Models.RequestResultM changeStatutMailToLu(int idMail)
        {
            Models.RequestResultM resultatRequete = new Models.RequestResultM();
            Models.Mail monMail = new Models.Mail { idMail = idMail };
           
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/MailAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var responseTask = client.PostAsJsonAsync("changeStatutMailToLu", monMail);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        resultatRequete.result = "OK";
                    }
                    else
                    {
                        resultatRequete.result = "NoOK";
                        var content = result.Content.ReadAsStringAsync();
                        content.Wait();
                        resultatRequete.msg = content.ToString();
                    }
                    return resultatRequete;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}