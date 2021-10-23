using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using votingSystem.Models;
using Xamarin.Essentials;

namespace votingSystem.Helpers
{
    /// <summary>
    /// Cette classe sera utilisé pour sauvergarder des données dans des fichiers json.
    /// </summary>
    public static class SaveManager
    {
        public enum TypeSave
        {
            InJsonFile,
            InPrefence
        }

        /// <summary>
        /// Enrégistre le token d'accès.
        /// </summary>
        /// <param name="elector"></param>
        /// <param name="_typeSave"></param>
        /// <returns></returns>
        public static bool Save_ElectorInfos_AfterEnrollement(ElectorInfo elector , int _typeSave = 1)
        {
            // Si elector est null return false.
            if (elector == null)
                return false;


            switch ((TypeSave)_typeSave)
            {
                case TypeSave.InJsonFile:
                    var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Constante.jsonFileName_ElectorInfos_AfterEnrollement);

                    string json = JsonConvert.SerializeObject(elector);
                    //write string to file
                    System.IO.File.WriteAllText(path, json);
                    return true;
                case TypeSave.InPrefence:
                    // On garde le token d'accès dans une preference.
                    Preferences.Set(Constante.keyPreference_AccesToken, elector.accessToken);
                    return true;
                default:
                    break;
            }

            return false;

        }

        /// <summary>
        /// Enregistre le electorLockCode.
        /// </summary>
        /// <param name="electorLockCode"></param>
        /// <returns></returns>
        public static bool Save_ElectorLockCode(string electorLockCode)
        {
            Preferences.Set(Constante.keyPreference_ElectorLockCode, electorLockCode);
            return true;
        }


        /// <summary>
        /// Enregistre le electorVote.
        /// </summary>
        /// <param name="electorVote"></param>
        /// <returns></returns>
        public static bool Save_ElectorVote(string electorVote)
        {
            Preferences.Set(Constante.keyPreference_ElectorVOte, electorVote);
            return true;
        }
    }
}
