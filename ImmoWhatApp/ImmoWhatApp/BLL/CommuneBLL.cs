﻿using ImmoWhatApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.BLL
{
    public class CommuneBLL
    {
        public static List<Models.Commune> ListeTtesCommunes = GetAllCommunesCompleteBLL();

        public static List<Models.Commune> GetAllCommunesCompleteBLL()
        {
            List<Models.Commune> Communes = new List<Models.Commune>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask = client.GetAsync("GetCommunesComplet");
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.Commune>>();
                        readTask.Wait();
                        var maListe = readTask.Result;
                        Communes = maListe.ToList();
                    }
                    else
                    {
                        Communes = new List<Models.Commune>();
                    }
                }
                return Communes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<Models.Commune> GetAllCommunesCompleteWithLanguageBLL(string langue)
        {
            List<Models.Commune> mesCommunes = GetAllCommunesCompleteBLL();
            List<Models.Commune> result = mesCommunes.FindAll(item => item.langue == langue);
            return result;
        }

        //public static List<Models.Commune> GetCommunesByNameBLL(string name)
        //{
        //    List<Models.Commune> ListCommunesCompl = new List<Models.Commune>();
        //    List<Models.Commune> ListeCommuneByName = new List<Models.Commune>();

        //    ListeCommuneByName = ListCommunesCompl.Find(x => x.Localite.StartsWith(name));




        //    return Communes;
        //}
        public static Models.Commune checkIfCommuneExistsBLL (string nomCommune)
        {
            try
            {
                Models.Commune result = ListeTtesCommunes.Find(item => item.Localite == nomCommune.ToLower());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public static int GetAveragePrice(int annee, string typeBien, string codePostale)
        {
            try
            {
                int moyennePrix;
                using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask = client.GetAsync("GetAveragePrice?annee="+annee+ "&typeBien="+typeBien+ "&codePostale="+codePostale);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<int>();
                        readTask.Wait();
                        moyennePrix = readTask.Result;
                    }
                    else
                    {
                        moyennePrix = -1;
                    }
                }
                return moyennePrix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetAverageClass(int annee, string typeBien, string codePostale)
        {
            try
            {
                int classePrix;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask = client.GetAsync("GetAverageClass?annee=" + annee + "&typeBien=" + typeBien + "&codePostale=" + codePostale);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<int>();
                        readTask.Wait();
                        classePrix = readTask.Result;
                    }
                    else
                    {
                        classePrix = -1;
                    }
                }
                return classePrix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}