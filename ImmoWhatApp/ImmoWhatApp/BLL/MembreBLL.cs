﻿using Newtonsoft.Json.Linq;
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
            Models.ResultInscription resultInscription = new Models.ResultInscription();

            

            // inscription du membre dans la DB
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

        public static Models.ResultRequest Connexion(Models.Identification moi)
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

                    resultRequest.idError = 0;
                    resultRequest.msg = "ok";
                }
            }
            var currentSession = HttpContext.Current.Session;
            var token = currentSession["monToken"];
            currentSession["monToken"] = MyToken;

            return resultRequest;
        }

        public static Models.MembreModels GetMyProfile(string mail)
        {
            using (var client = new HttpClient())
            {
                Models.MembreModels moi = new Models.MembreModels();
                client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                var responseTask = client.GetAsync("GetMyProfile?mail=" + mail);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Models.MembreModels>();
                    readTask.Wait();

                    moi = readTask.Result;
                }
                return moi;
            }
        }

        
    }
}