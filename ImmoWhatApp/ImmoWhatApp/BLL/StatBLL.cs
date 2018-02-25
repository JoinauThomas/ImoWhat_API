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
    }
}