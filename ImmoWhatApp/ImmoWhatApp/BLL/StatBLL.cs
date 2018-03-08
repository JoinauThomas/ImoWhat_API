using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ImmoWhatApp.BLL
{
    public class StatBLL
    {
        public static List<Models.TableResultGraphic> GetTableGraphique(string codePostale)
        {
            List<Models.TableResultGraphic> tableResultGraph = new List<Models.TableResultGraphic>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/StatApi/");
                    var responseTask = client.GetAsync("GetTableGraphique?codePostal=" + codePostale);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.TableResultGraphic>>();
                        readTask.Wait();
                        tableResultGraph = readTask.Result;

                        return tableResultGraph;
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

        public static List<Models.TableGraphTransactionsModels> GetTableGraphiqueTransaction(string codePostale)
        {
            List<Models.TableGraphTransactionsModels> tableResultGraph = new List<Models.TableGraphTransactionsModels>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/StatApi/");
                    var responseTask = client.GetAsync("GetTableGraphiqueTransactions?codePostal=" + codePostale);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.TableGraphTransactionsModels>>();
                        readTask.Wait();
                        tableResultGraph = readTask.Result;

                        return tableResultGraph;
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

        public static List<Models.tablePriceStat> GetPriceTable(int anneeRecherchee, string codePostal)
        {
            List<Models.tablePriceStat> resultTable = new List<Models.tablePriceStat>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/StatApi/");
                    var responseTask = client.GetAsync("GetPriceTable?anneeRecherchee=" + anneeRecherchee+ "&codePostal=" + codePostal);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.tablePriceStat>>();
                        readTask.Wait();
                        resultTable = readTask.Result;

                        return resultTable;
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
        public static List<Models.tablePriceStat> GetAverageAndTransactionsTable(int anneeRecherchee, string codePostal)
        {
            List<Models.tablePriceStat> resultTable = new List<Models.tablePriceStat>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/StatApi/");
                    var responseTask = client.GetAsync("GetAverageAndTransactionsTable?anneeRecherchee=" + anneeRecherchee + "&codePostal=" + codePostal);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.tablePriceStat>>();
                        readTask.Wait();
                        resultTable = readTask.Result;

                        return resultTable;
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

        public static List<Models.tablePriceStat> GetTableTransactions(int anneeRecherchee, string codePostal)
        {
            List<Models.tablePriceStat> resultTable = new List<Models.tablePriceStat>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/StatApi/");
                    var responseTask = client.GetAsync("GetTableTransactions?anneeRecherchee=" + anneeRecherchee + "&codePostal=" + codePostal);
                    var result = responseTask.Result;
                    responseTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.tablePriceStat>>();
                        readTask.Wait();
                        resultTable = readTask.Result;

                        return resultTable;
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
    }
}