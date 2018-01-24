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
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult CreerCompte()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult CreerCpte()
        {
            return View();
        }
        

        [HttpPost]
        public void CreerCpte(Models.Identification moi)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49383");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var formContent = new FormUrlEncodedContent(new[]
                {
new KeyValuePair<string, string>("Email", "jt@ephec.be"),
new KeyValuePair<string, string>("Password", "Password-2017"),
new KeyValuePair<string, string>("ConfirmPassword", "Password-2017")
});
                var responseTask = client.PostAsync("api/Account/Register", formContent);
                responseTask.Wait();
                var result = responseTask.Result;
            }
        }

        [HttpPost]
        public ActionResult CreerCompte(Models.MembreModels newMembre)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider test = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(newMembre.pswd);
            data = test.ComputeHash(data);
            String md5Hash = System.Text.Encoding.ASCII.GetString(data);
            newMembre.pswd = md5Hash;

            string Result = BLL.MembreBLL.CreerCompte(newMembre);


            if (Result == "ok")
            {
                return RedirectToAction("nvBien");
            }
            else
            {
                ViewBag.Error = Result;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Connexion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Connexion(Models.Identification moi)
        {
            var MyToken = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49383");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "jt@ephec.be"),
                    new KeyValuePair<string, string>("password", "Password-2017"),
                });
                var responseTask = client.PostAsync("/api/MyGetToken", formContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    var responseString = result.Content.ReadAsStringAsync();
                    var res = responseString.Result;
                }
                else
                {
                    var responseString = result.Content.ReadAsStringAsync();
                    responseString.Wait();
                    //get access token from response body
                    var jObject = JObject.Parse(responseString.Result);
                    MyToken = jObject.GetValue("access_token").ToString();
                }
            }
            Session["monToken"] = MyToken;
            
            return View();
        }


    }
}