using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ImmoWhatApp.BLL
{
    public class MembreBLL
    {
        public static Models.ResultInscription CreerCompte(Models.MembreModels newMembre)
        {
            try
            {
                if(newMembre.boite == null)
                {
                    newMembre.boite = "-1";
                }
                Models.ResultInscription resultInscription = new Models.ResultInscription();

                // inscription d'un nouveau USER dans la DB

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("Email", newMembre.mail),
                    new KeyValuePair<string, string>("Password", newMembre.pswd),
                    new KeyValuePair<string, string>("ConfirmPassword", newMembre.pswd)
                });
                    var responseTask = client.PostAsync("api/Account/Register", formContent);
                    responseTask.Wait();
                    var result = responseTask.Result;
                }

                // creation d'un nouveau MEMBRE dans la DB

                using (var client = new HttpClient())
                {
                    //var currentSession = HttpContext.Current.Session;
                    //var token = currentSession["monToken"];

                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var responseTask = client.PostAsJsonAsync("AddNewMembre", newMembre);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        resultInscription.resultQuery = "ok";
                    }
                    else
                    {
                        var content = result.Content.ReadAsStringAsync();
                        content.Wait();
                        resultInscription.resultQuery = content.ToString();
                    }
                }

                // recuperation de l'ID de l'User créé

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");

                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    //var responseTask = client.PostAsJsonAsync("GetMyId", mail);
                    var responseTask = client.GetAsync("GetMyId?mail=" + newMembre.mail);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<int>();
                        readTask.Wait();
                        resultInscription.idMembre = readTask.Result;
                    }
                    else
                    {
                        resultInscription.idMembre = 0;
                    }
                }

                return resultInscription;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static Models.ResultRequest Connexion(Models.Identification moi)
        {
            try
            {
                var MyToken = "";
                Models.ResultRequest resultRequest = new Models.ResultRequest();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", moi.login),
                    new KeyValuePair<string, string>("password", moi.password),
                });
                    var responseTask = client.PostAsync("/api/MyGetToken", formContent);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (!result.IsSuccessStatusCode)
                    {
                        var responseString = result.Content.ReadAsStringAsync();
                        resultRequest.msg = responseString.Result;
                        resultRequest.idError = responseString.Id;

                        return resultRequest;
                    }
                    else
                    {
                        var responseString = result.Content.ReadAsStringAsync();
                        responseString.Wait();
                        //get access token from response body
                        var jObject = JObject.Parse(responseString.Result);
                        MyToken = jObject.GetValue("access_token").ToString();
                        CookieHeaderValue cookie = new CookieHeaderValue("myToken", MyToken);
                       

                        resultRequest.idError = 0;
                        resultRequest.msg = "ok";
                    }
                }
                HttpCookie myToken = new HttpCookie("myToken");
                myToken.Value = MyToken;
                HttpContext.Current.Response.Cookies.Add(myToken);

                var currentSession = HttpContext.Current.Session;
                var token = currentSession["monToken"];
                currentSession["monToken"] = MyToken;

                return resultRequest;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public static Models.MembreModels GetMyProfile(string mail)
        {
            try
            {
               

                using (var client = new HttpClient())
                {
                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    var responseTask = client.GetAsync("GetMyProfile?mail=" + mail);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Models.MembreModels>();
                        readTask.Wait();

                        moi = readTask.Result;
                        return moi;
                    }
                    else
                    {
                        var responseString = result.Content.ReadAsStringAsync();
                        var res = responseString.Result;
                        return null;
                    }
                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckIfMailExists(string mail)
        {
            bool res;
            try
            {
                using (var client = new HttpClient())
                {
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    var responseTask = client.GetAsync("CheckIfMailExists?mail=" + mail);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<bool>();
                        readTask.Wait();

                        res = readTask.Result;
                        return res;
                    }
                    else
                    {
                        var responseString = result.Content.ReadAsStringAsync();
                        var error = responseString.Result;
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Models.RequestResultM GetCountOfNewMails(int idMembre)
        {
            Models.RequestResultM resultRequest = new Models.RequestResultM();

            try
            {
                using (var client = new HttpClient())
                {
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
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
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
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
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
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
    }
}