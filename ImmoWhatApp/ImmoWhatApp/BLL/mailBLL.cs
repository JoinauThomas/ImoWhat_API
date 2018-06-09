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
        public static Models.RequestResultM GetCountOfNewMails(int idMembre)
        {
            Models.RequestResultM resultRequest = new Models.RequestResultM();

            try
            {
                using (var client = new HttpClient())
                {
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MailAPI/");
                    var responseTask = client.GetAsync("GetCountOfNewMails?idMembre=" + idMembre);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<int>();
                        readTask.Wait();

                        int nbMsg = readTask.Result;

                        resultRequest.result = "OK";
                        resultRequest.resultRequest = nbMsg;
                        return resultRequest;
                    }
                    else
                    {
                        resultRequest.result = "NoOK";
                        var responseString = result.Content.ReadAsStringAsync();
                        resultRequest.msg = responseString.Result;
                        return resultRequest;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Models.RequestResultM sendMail(Models.Mail newMail)
        {
            Models.RequestResultM resultMail = new Models.RequestResultM();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/MailAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var responseTask = client.PostAsJsonAsync("sendMail", newMail);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        resultMail.result = "ok";
                    }
                    else
                    {
                        resultMail.result = "NoOK";
                        var content = result.Content.ReadAsStringAsync();
                        content.Wait();
                        resultMail.msg = content.ToString();
                    }
                    return resultMail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Models.RequestResultM GetListOfMails(int idMembre)
        {
            try
            {
                Models.RequestResultM resultRequest = new Models.RequestResultM();
                List<Models.Mail> listeMails = new List<Models.Mail>();

                using (var client = new HttpClient())
                {
                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    client.BaseAddress = new Uri("http://localhost:49383/api/MailAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    var responseTask = client.GetAsync("GetListOfMails?idMembre=" + idMembre);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.Mail>>();
                        readTask.Wait();

                        listeMails = readTask.Result;
                        resultRequest.result = "OK";
                        resultRequest.listeMails = listeMails;
                        return resultRequest;
                    }
                    else
                    {
                        resultRequest.result = "NoOk";
                        var responseString = result.Content.ReadAsStringAsync();
                        resultRequest.msg = responseString.Result;
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        public static Models.RequestResultM changeStatutMailToDeleted(int idMail)
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
                    var responseTask = client.PostAsJsonAsync("changeStatutMailToDeleted", monMail);
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