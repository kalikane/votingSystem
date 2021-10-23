using MsgPack.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using votingSystem.Helpers;
using votingSystem.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace votingSystem.Services
{
    public class VotingService //: IRestService
    {
        HttpClient client;

        public VotingService()
        {
            client = new HttpClient();

        }

        /// <summary>
        /// Lance l'opération d'enrollement.
        /// </summary>
        /// <param name="_voterInfo"></param>
        /// <returns></returns>
        public async Task<bool> EnrollVoterAsync(VoterEnrollement _voterInfo)
        {
            if (_voterInfo == null)
                return false;

            Uri uri = new Uri("http://130.61.52.87:8090/enrollment/create");

            //string json = JsonConvert.SerializeObject(_voterInfo);
            //var options = new JsonSerializerOptions { WriteIndented = true };
            //string json = System.Text.Json.JsonSerializer.Serialize(_voterInfo, options);

            string jsonString = "" +
                "{\"cni\": " + "\"" + _voterInfo.cni + "\"" + "," +
                "\"name\": " + "\"" + _voterInfo.name + "\"" + "," +
                "\"surname\": " + "\"" + _voterInfo.surname + "\"" + "," +
                "\"birthday\": " + "\"" + _voterInfo.birthday + "\"" + "," +
                "\"birthPlace\": " + "\"" + _voterInfo.birthPlace + "\"" + "," +
                "\"fatherName\": " + "\"" + _voterInfo.fatherName + "\"" + "," +
                "\"motherName\": " + "\"" + _voterInfo.motherName + "\"" + "}";

            //Console.WriteLine($"jsonString = {jsonString}");
            //Console.WriteLine("***************************************");

            ////string json = jsonString.Replace(@"\", string.Empty);
            //Console.WriteLine($"Json = {json}");
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");



            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);


            if (response.IsSuccessStatusCode)
            {
                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();

                // Si le resultat n'est pas vide on Deserialize le string vers la Data
                var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiReponseData<ElectorInfo>>(result);
                if (SaveManager.Save_ElectorInfos_AfterEnrollement(responseObject.data, (int)SaveManager.TypeSave.InPrefence))
                {
                    return true;
                }
                else
                    return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                await Application.Current.MainPage.DisplayAlert("Inscription", response.RequestMessage.Content.ToString(), "OK");
                return false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Inscription", response.RequestMessage.Content.ToString(), "OK");
                return false;
            }

        }

        /// <summary>
        /// Lance le process d'authentification.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="officeCode"></param>
        /// <returns></returns>
        public async Task<bool> AuthenticationVoterAsync(string accessToken, string officeCode)
        {
            // Si l'accesToken ou officeCode est null return false.
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(officeCode)) return false;

            // Créer l'uri à partir de l'url d'authentification.
            Uri uri = new Uri(Constante.Url_VoterAuthentification);

            // Construire le Json qui ira avec le requete et qui aura les infos passées en paramètre.
            string jsonToPost = "{\"accessToken\":\" " + accessToken + "\"," +
                                 "\"officeCode\": \" " + officeCode + " \"}";
            // Instancier le StringContent.
            StringContent content = new StringContent(jsonToPost, Encoding.UTF8, "application/json");

            // Instancier le HttpResponseMessage.
            HttpResponseMessage response = null;

            // lancer la requète Post.
            response = await client.PostAsync(uri, content);

            // Si le statut code est bon désérializé l'information
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiReponseData<string>>(result);

                // stocker le code electeur dans les préférences.
                if (SaveManager.Save_ElectorLockCode(responseObject.data))
                {
                    return true;
                }
                else
                    return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                await Application.Current.MainPage.DisplayAlert("Authentification de l'électeur", response.RequestMessage.Content.ToString(), "OK");
                return false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Authentification de l'électeur", response.RequestMessage.Content.ToString(), "OK");
                return false;
            }

        }

        /// <summary>
        /// Récupère la liste des bureaux de vote.
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<VotingOfficeInfo>> GetVotingOfficeAsync()
        {

            var votingOffices = new ObservableCollection<VotingOfficeInfo>();

            Uri uri = new Uri(string.Format(Constante.Url_GetVotingOffice));

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Contenu: {content}");
                var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiReponseDatas<VotingOfficeInfo>>(content);

                foreach (var item in responseObject.data)
                {
                    votingOffices.Add(item);
                }
            }
            return votingOffices;

        }



        /// <summary>
        /// Retourne un string aléatoire
        /// </summary>
        /// <returns></returns>
        public string GetRandomString(int taille = 8)
        {
            // Déclaration d'un string builder.
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            // Génération d'un float aléatoire
            var myFloat = random.NextDouble();

            for (int i = 0; i < taille; i++)
            {
                var myChar = Convert.ToChar(Convert.ToInt32(Math.Floor(25 * myFloat) + 65));
                stringBuilder.Append(myChar);
            }
            return stringBuilder.ToString();
        }



        /// <summary>
        /// Récupère la liste des bulletins de votes de l'électeur.
        /// </summary>
        /// <param name="electorRandomValue"></param>
        /// <param name="electorLockCode"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<BulletinVote>> GetBulletinsVote(string electorRandomValue, string electorLockCode)
        {
            if (string.IsNullOrEmpty(electorLockCode) || string.IsNullOrEmpty(electorRandomValue)) return null;

            Console.WriteLine($"rs={electorRandomValue} elc = {electorLockCode}");
            var bulletinsVote = new ObservableCollection<BulletinVote>();

            Uri uri = new Uri(string.Format(Constante.Url_GetBulletinsVote, string.Empty));

            // Construire le Json qui ira avec le requete et qui aura les infos passées en paramètre.
            string jsonToPost = "{\"electorRandomValue\":\" " + electorRandomValue + "\"," +
                                 "\"electorLockCode\": \" " + electorLockCode + " \"}";


            // Instancier le StringContent.
            StringContent content = new StringContent(jsonToPost, Encoding.UTF8, "application/json");

            // Instancier le HttpResponseMessage.
            HttpResponseMessage response = null;


            // lancer la requète Post.
            response = await client.PostAsync(uri, content);



            if (response.IsSuccessStatusCode)
            {

                string result = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Contenu: {content}");
                var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiReponseDatas<BulletinVote>>(result);

                foreach (var item in responseObject.data)
                {
                    bulletinsVote.Add(item);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", $"Un probleme est survenu avec statuCode: {response.StatusCode}", "OK");
            }
            return bulletinsVote;
        }

        /// <summary>
        /// Lance la requete de vote.
        /// </summary>
        /// <returns></returns>
        public async Task VoteAsync(string candidatChoice, string lockCode, string randomCode)
        {
            if (string.IsNullOrEmpty(candidatChoice) || string.IsNullOrEmpty(lockCode) || string.IsNullOrEmpty(lockCode))
                return;

            Uri uri = new Uri(Constante.Url_ElectorVote);
            string json = "{\" candidatChoice \" : \"" + candidatChoice + "\"," +
                "\" lockCode \" : \"" + lockCode + "\"," +
                "\" randomCode \" : \"" + randomCode + "\"}";




            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);


            if (response.IsSuccessStatusCode)
            {
                SaveManager.Save_ElectorLockCode(candidatChoice);
                await Application.Current.MainPage.DisplayAlert("CONFIRMATION", "Merci, Votre vote a bien été enrégistrer", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", $"Une erreur du type: {response.StatusCode} est survenu", "OK");
                return;

            }

        }
    }
}
