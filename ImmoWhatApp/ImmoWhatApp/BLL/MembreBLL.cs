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

        public static Models.ResultRequest EnregistrerNvlleConnection( Models.Identification moi )
        {
            try
            {
                Models.ResultRequest resultRequest = new Models.ResultRequest();
                using (var client = new HttpClient())
                {

                    // enregistrement de la connection dans la DB

                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var responseTask2 = client.PostAsJsonAsync("EnregistrerNvlleConnection", moi);
                    responseTask2.Wait();
                    var result2 = responseTask2.Result;

                    if (!result2.IsSuccessStatusCode)
                    {
                        var responseString2 = result2.Content.ReadAsStringAsync();
                        resultRequest.msg = responseString2.Result;
                        resultRequest.idError = responseString2.Id;

                        return resultRequest;
                    }
                    else
                    {
                        resultRequest.idError = 0;
                        resultRequest.msg = "ok";
                        return resultRequest;
                    }
                }
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

        public static List<Models.RechercheMembreModels> SearchMembres(string recherche)
        {
            List<Models.RechercheMembreModels> lesMembres = new List<Models.RechercheMembreModels>();
            try
            {
                if (recherche == null)
                    recherche = "";


                using (var client = new HttpClient())
                {

                    // CHECK IF MEMBER IS CONNECTED

                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    // GET THE LIST OF BIENs

                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    var responseTask = client.GetAsync("SearchMembres?recherche=" + recherche);
                    responseTask.Wait();
                    var result = responseTask.Result;


                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.RechercheMembreModels>>();
                        readTask.Wait();
                        lesMembres = readTask.Result;


                    }
                    return lesMembres;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}