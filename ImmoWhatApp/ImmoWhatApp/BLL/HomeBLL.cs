using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ImmoWhatApp.BLL
{
    public class HomeBLL
    {

        public static int GetHighestYear()
        {
            int annee;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/HomeApi/");
                    var responseTask = client.GetAsync("GetHighestYear");
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<int>();
                        readTask.Wait();
                        annee = readTask.Result;
                    }
                    else
                    {
                        annee = -1;
                    }
                }
                return annee;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
