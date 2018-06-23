using System;
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
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


                    // CHECK SI ADRESSE EXISTE DEJA

                    Models.adresseModels adresse = new Models.adresseModels { CodePostal = newBien.codePostale, Rue = newBien.rue, Numero = newBien.numero, Boite = newBien.boite };
                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.GetAsync("CheckIfAdressExists?codePostale=" + adresse.CodePostal + "&rue=" + adresse.Rue + "&numero=" + adresse.Numero + "&boite=" + adresse.Boite);
                    responseTask.Wait();
                    var result = responseTask.Result;


                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<bool>();
                        readTask.Wait();
                        bool exist = readTask.Result;

                        // SI ELLE N EXISTE PAS ENCORE, ON CREE LA NVELLE ADRESSE

                        if (exist == false)
                        {

                            using (var client2 = new HttpClient())
                            {


                                client2.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client2.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

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

        public static int GetIdBien(Models.adresseModels adresse)
        {
            int id = -1;
            try
            {
                using (var client = new HttpClient())
                {
                    // CHECK IF MEMBER IS CONNECTED

                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);



                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.GetAsync("GetIdBien?codePostale=" + adresse.CodePostal + "&rue=" + adresse.Rue + "&numero=" + adresse.Numero + "&boite=" + adresse.Boite);
                    responseTask.Wait();
                    var result = responseTask.Result;


                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<int>();
                        readTask.Wait();
                        id = readTask.Result;

                        // SI ELLE N EXISTE PAS ENCORE, ON CREE LA NVELLE ADRESSE

                    }
                    return id;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Models.RequestResultM PostNewPhotos(int idBien, List<string> liens)
        {
            Models.RequestResultM resultatRequete;
            Models.imageModels images = new Models.imageModels { idBien = idBien, liens = liens };
            try
            {
                using (var client = new HttpClient())
                {
                    // CHECK IF MEMBER IS CONNECTED

                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.PostAsJsonAsync("PostNewPhotos", images);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        resultatRequete = new Models.RequestResultM { result = "OK", msg = "photo bien enregistree" };

                        return resultatRequete;
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

        public static List<Models.BienModels> GetListBiensFromCPAndType(string codePostale, int type)
        {
            List<Models.BienModels> listeDeBiens = new List<Models.BienModels>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.GetAsync("GetListBiensFromCPAndType?codePostale=" + codePostale + "&type=" + type);
                    responseTask.Wait();
                    var result = responseTask.Result;


                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.BienModels>>();
                        readTask.Wait();
                        listeDeBiens = readTask.Result;


                    }
                    return listeDeBiens;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Models.BienModels GetOneBien(int idBien)
        {
            Models.BienModels TheBien = new Models.BienModels();
            try
            {
                using (var client = new HttpClient())
                {
                    // CHECK IF MEMBER IS CONNECTED

                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    // GET THE BIEN

                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.GetAsync("GetOneBien?idBien=" + idBien);
                    responseTask.Wait();
                    var result = responseTask.Result;


                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Models.BienModels>();
                        readTask.Wait();
                        TheBien = readTask.Result;


                    }
                    return TheBien;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Models.BienModels> GetMyBiensList(int idMembre)
        {
            List<Models.BienModels> listeDeBiens = new List<Models.BienModels>();
            try
            {
                using (var client = new HttpClient())
                {

                    // CHECK IF MEMBER IS CONNECTED

                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    // GET THE LIST OF BIENs

                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.GetAsync("GetMyBiensList?idMembre=" + idMembre);
                    responseTask.Wait();
                    var result = responseTask.Result;


                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.BienModels>>();
                        readTask.Wait();
                        listeDeBiens = readTask.Result;


                    }
                    return listeDeBiens;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Models.RequestResultM DeleteBienByUser(int idBien)
        {
            Models.RequestResultM resultatRequete;
            Models.BienModels bienASupprimer = new Models.BienModels { idBien = idBien };
            try
            {
                using (var client = new HttpClient())
                {
                    // CHECK IF MEMBER IS CONNECTED

                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.PostAsJsonAsync("DeleteBienByUser", bienASupprimer);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        resultatRequete = new Models.RequestResultM { result = "OK", msg = "Le bien a bien été supprimé" };

                        return resultatRequete;
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

        public static Models.RequestResultM DeclareBienAsVendu(int idBien)
        {
            Models.RequestResultM resultatRequete;
            Models.BienModels bienASupprimer = new Models.BienModels { idBien = idBien };
            try
            {
                using (var client = new HttpClient())
                {
                    // CHECK IF MEMBER IS CONNECTED

                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.PostAsJsonAsync("DeclareBienAsVendu", bienASupprimer);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        resultatRequete = new Models.RequestResultM { result = "OK", msg = "Le bien a bien été supprimé" };

                        return resultatRequete;
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

        public static List<Models.RechercheBienModels> SearchBien(string recherche)
        {
            List<Models.RechercheBienModels> lesBiens = new List<Models.RechercheBienModels>();
            try
            {
                if (recherche == null)
                    recherche = "";


                using (var client = new HttpClient())
                {

                    // CHECK IF MEMBER IS CONNECTED

                    var currentSession = HttpContext.Current.Session;
                    var token = currentSession["monToken"];
                    Models.MembreModels moi = new Models.MembreModels();
                    client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    // GET THE LIST OF BIENs

                    client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                    var responseTask = client.GetAsync("SearchBiens?recherche=" + recherche);
                    responseTask.Wait();
                    var result = responseTask.Result;


                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Models.RechercheBienModels>>();
                        readTask.Wait();
                        lesBiens = readTask.Result;


                    }
                    return lesBiens;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Models.RequestResultM DeleteBienByAdmin(int idBien)
        {
            Models.BienModels bienASupprimer = new Models.BienModels { idBien = idBien };
            Models.RequestResultM resultat = new Models.RequestResultM();

            using (var client = new HttpClient())
            {
                var currentSession = HttpContext.Current.Session;
                var token = currentSession["monToken"];
                Models.MembreModels moi = new Models.MembreModels();
                client.BaseAddress = new Uri("http://localhost:49383/api/MembreAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


                client.BaseAddress = new Uri("http://localhost:49383/api/BienAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.PostAsJsonAsync("DeleteBienByAdmin", bienASupprimer);
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    resultat.result = "OK";
                }
                else
                {
                    var content = result.Content.ReadAsStringAsync();
                    content.Wait();
                    resultat.result = "NotOK";
                    resultat.msg = content.ToString();
                }

                return resultat;
            }
        }

    }
}