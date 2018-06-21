using ImmoWhatApp.Models;
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

        public static Models.Commune GetACommuneWithCodePostal (string codePostal, string langue)
        {
            try
            {
                Models.Commune maCommune = new Commune();
               
                using (var client = new HttpClient())
                {
                    // récupération du tableau des prix
                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask1 = client.GetAsync("GetACommuneWithCodePostal?codePostal=" + codePostal+"&langue="+langue);
                    var resultTable = responseTask1.Result;
                    responseTask1.Wait();

                    if (resultTable.IsSuccessStatusCode)
                    {
                        var readTask = resultTable.Content.ReadAsAsync<Models.Commune>();
                        readTask.Wait();
                        maCommune = readTask.Result;
                    }
                    else
                    {
                        maCommune = null;
                    }
                    
                }
                return maCommune;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        public static int GetAverageClass2(int annee, string typeBien, string codePostale)
        {
            try
            {
                List<Models.TablePrice> tablePrix = new List<TablePrice>();
                int classePrix;
                using (var client = new HttpClient())
                {
                    // récupération du tableau des prix
                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask1 = client.GetAsync("GetTableClassPrice?typeBien=" + typeBien);
                    var resultTable = responseTask1.Result;
                    responseTask1.Wait();

                    if (resultTable.IsSuccessStatusCode)
                    {
                        var readTask = resultTable.Content.ReadAsAsync<List<Models.TablePrice>>();
                        readTask.Wait();
                        tablePrix = readTask.Result.ToList();
                    }
                    else
                    {
                        classePrix = -1;
                    }


                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask = client.GetAsync("GetInfoMainMenu?annee=" + annee + "&typeBien=" + typeBien + "&codePostale=" + codePostale);
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




        public static List<Models.TablePrice> GetTablePrice(string typeBien)
        {
            try
            {
                List<Models.TablePrice> tablePrix = new List<TablePrice>();
                using (var client = new HttpClient())
                {
                    // récupération du tableau des prix
                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask1 = client.GetAsync("GetTableClassPrice?typeBien=" + typeBien);
                    var resultTable = responseTask1.Result;
                    responseTask1.Wait();

                    if (resultTable.IsSuccessStatusCode)
                    {
                        var readTask = resultTable.Content.ReadAsAsync<List<Models.TablePrice>>();
                        readTask.Wait();
                        tablePrix = readTask.Result.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                return tablePrix;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetIdClassPrix(int annee, string typeBien, string codePostale)
        {
            int classePrix;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask = client.GetAsync("GetInfoMainMenu?annee=" + annee + "&typeBien=" + typeBien + "&codePostale=" + codePostale);
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

        public static int GetAveragePrice(int annee, string typeBien, string codePostale)
        {
            try
            {
                int moyennePrix;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask = client.GetAsync("GetAveragePrice?annee=" + annee + "&typeBien=" + typeBien + "&codePostale=" + codePostale);
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

        public static List<Models.tableStatModels> GetInfoEvolutionPrices(string codePostale)
        {
            List<Models.tableStatModels> tableStatPrixCommune = new List<tableStatModels>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask = client.GetAsync("GetInfoEvolutionPrices?codePostal=" + codePostale);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.tableStatModels>>();
                        readTask.Wait();
                        tableStatPrixCommune = readTask.Result;

                        return tableStatPrixCommune;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static Models.CommuneContourPoint GetCommuneContourPoints(int idType)
        {
            Models.CommuneContourPoint ListePoints = new Models.CommuneContourPoint();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask = client.GetAsync("GetCommuneContourPoints?idType="+idType);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Models.CommuneContourPoint>();
                        readTask.Wait();
                        ListePoints = readTask.Result;
                    }
                    else
                    {
                        ListePoints = new Models.CommuneContourPoint();
                    }
                }
                return ListePoints;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Models.StatCommune GetInfosCommune(string commune, int age)
        {

            Models.StatCommune statCommune = new Models.StatCommune();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/CommuneApi/");
                    var responseTask = client.GetAsync("GetInfosCommune?commune="+commune+"&age="+age);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Models.StatCommune>();
                        readTask.Wait();
                        statCommune = readTask.Result;
                    }
                    else
                    {
                        statCommune = null;
                    }
                }
                return statCommune;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}