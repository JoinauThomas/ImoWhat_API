﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace ImmoWhatApp.BLL
{
    public class BienBLL
    {
        public static Models.RequestResultM addNewBien(Models.BienModels newBien)
        {
            Models.RequestResultM resultatRequete = new Models.RequestResultM();
            try
            {
                using (var client = new HttpClient())
                {
                    // CHECK IF MEMBER IS CONNECTED

                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    string tokens = token.ToString();

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens);


                    // CHECK SI ADRESSE EXISTE DEJA

                    Models.adresseModels adresse = new Models.adresseModels { CodePostal = newBien.codePostal, Rue = newBien.rue, Numero = newBien.numero, Boite = newBien.boite };
                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.GetAsync("CheckIfAdressExists?codePostale=" + adresse.CodePostal+"&rue="+adresse.Rue+"&numero="+adresse.Numero+"&boite="+adresse.Boite);
                    responseTask.Wait();
                    var result = responseTask.Result;


                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<bool>();
                        readTask.Wait();
                        bool exist = readTask.Result;

                        // SI ELLE N EXISTE PAS ENCORE, ON CREE LA NVELLE ADRESSE

                        if(exist == false)
                        {
                            Models.MembreModels moi = new Models.MembreModels();
                            //client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                            //client.DefaultRequestHeaders.Accept.Clear();
                            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                            using (var client2 = new HttpClient())
                            {
                                client2.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                                var responseTask2 = client.PostAsJsonAsync("addNewBien", newBien);
                                responseTask2.Wait();
                                var result2 = responseTask2.Result;

                                if (result2.IsSuccessStatusCode)
                                {

                                    resultatRequete = new Models.RequestResultM { result = "OK", msg = Resource.LeBienABienEteEnregistre };
                                    return resultatRequete;

                                }
                                else
                                {
                                    var responseString = result2.Content.ReadAsStringAsync();
                                    resultatRequete = new Models.RequestResultM { result = "NOTOK", msg = responseString.Result };
                                    return resultatRequete;
                                }
                               
                            }
                        }
                        else
                        {
                            resultatRequete = new Models.RequestResultM { result = "NOTOK", msg = Resource.LeBienExisteDeja }; 
                            return resultatRequete;
                        }
                       
                    }
                    else
                    {
                        var responseString = result.Content.ReadAsStringAsync();
                        resultatRequete = new Models.RequestResultM { result = "NOTOK", msg = responseString.Result };
                        return resultatRequete;
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