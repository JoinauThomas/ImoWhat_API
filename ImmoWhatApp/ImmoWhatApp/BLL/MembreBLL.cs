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
        public static string CreerCompte(Models.MembreModels newMembre)
        {
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
            using (var client = new HttpClient())
            {
                //var currentSession = HttpContext.Current.Session;
                //var token = currentSession["monToken"];

                client.BaseAddress = new Uri("http://localhost:49383/api/Membre/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.PostAsJsonAsync("AddNewMembre", newMembre);
                responseTask.Wait();
                var result = responseTask.Result;

                
                if (result.IsSuccessStatusCode)
                {
                    return "ok";
                }
                else
                {
                    var content = result.Content.ReadAsStringAsync();
                    content.Wait();
                    return content.ToString();
                }
            }
            //HttpCookie monCookie = Request.Cookies["monToken"];
            //string token = "";
            //if (monCookie != null)
            //{
            //    token = monCookie["monToken"];
            //}
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:49383");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //    var responseTask = client.GetAsync("api/AddNewMembre");
            //    responseTask.Wait();
            //    var result = responseTask.Result;
            //    var responseString = result.Content.ReadAsStringAsync();
            //}
        }

        public static string Connexion(Models.Identification moi)
        {
            var MyToken = "";
            string resultat = "";

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
                    resultat = responseString.Result;
                }
                else
                {
                    var responseString = result.Content.ReadAsStringAsync();
                    responseString.Wait();
                    //get access token from response body
                    var jObject = JObject.Parse(responseString.Result);
                    MyToken = jObject.GetValue("access_token").ToString();

                    resultat = "ok";
                }
            }
            var currentSession = HttpContext.Current.Session;
            var token = currentSession["monToken"];
            currentSession["monToken"] = MyToken;

            return resultat;
        }
    }
}