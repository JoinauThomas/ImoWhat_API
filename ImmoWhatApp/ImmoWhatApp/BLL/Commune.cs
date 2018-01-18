using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.BLL
{
    public class Commune
    {
        public static List<Models.Commune> GetAllCommunesCompleteBLL()
        {
            List<Models.Commune> Communes = new List<Models.Commune>();

            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri("http://localhost:49383/api/Commune/");
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
    }
}