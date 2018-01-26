using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ImmoWhatApp.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        public ActionResult CreerCompte()
        {
            
            return View();
        }
        

      

        //[HttpGet]
        //public ActionResult Connexion()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Connexion(Models.Identification moi)
        //{
        //    var MyToken = "";
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:49383");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        var formContent = new FormUrlEncodedContent(new[]
        //        {
        //            new KeyValuePair<string, string>("grant_type", "password"),
        //            new KeyValuePair<string, string>("username", "jt@ephec.be"),
        //            new KeyValuePair<string, string>("password", "Password-2017"),
        //        });
        //        var responseTask = client.PostAsync("/api/MyGetToken", formContent);
        //        responseTask.Wait();
        //        var result = responseTask.Result;
        //        if (!result.IsSuccessStatusCode)
        //        {
        //            var responseString = result.Content.ReadAsStringAsync();
        //            var res = responseString.Result;
        //        }
        //        else
        //        {
        //            var responseString = result.Content.ReadAsStringAsync();
        //            responseString.Wait();
        //            //get access token from response body
        //            var jObject = JObject.Parse(responseString.Result);
        //            MyToken = jObject.GetValue("access_token").ToString();
        //        }
        //    }
        //    Session["monToken"] = MyToken;
            
        //    return View();
        //}


    }
}