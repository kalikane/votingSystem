using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace votingSystem.Helpers
{
    public static class Constante
    {
        /// <summary>
        /// Route de l'enrollement de l'électeur.
        /// </summary>
        public static string Url_Enrollement = "https://test-back.usurvey.upowa.org/enrollment/create";

        /// <summary>
        /// Route pour obtenir les bureaux de vote.
        /// </summary>
        public static string Url_GetVotingOffice = "https://test-back.usurvey.upowa.org/config/all/office";

        /// <summary>
        /// Route pour l'authentification de l'électeur.
        /// </summary>
        public static string Url_VoterAuthentification = "https://test-back.usurvey.upowa.org/auth/elector";

        public static string Url_GetBulletinsVote = "https://test-back.usurvey.upowa.org/vote/create/ballot";

        /// <summary>
        /// Routee pour récupérer le vote de l'électeur.
        /// </summary>
        public static string Url_ElectorVote = "https://test-back.usurvey.upowa.org/vote/create";



        public static string jsonFileName_ElectorInfos_AfterEnrollement = "ElectorInfos_AfterEnrollement_.json";

        /// <summary>
        /// Clé utilisé pour sauvegarder la valeur du token d'accès du l'électeur dans les preferences.
        /// </summary>
        public static string keyPreference_AccesToken = "accessToken";

        /// <summary>
        /// Clé préférences pour récupérer le electorLockCode.
        /// </summary>
        public static string keyPreference_ElectorLockCode = "electorLockCode";

        /// <summary>
        /// Clé préférences pour récupérer le electorVote.
        /// </summary>
        public static string keyPreference_ElectorVOte = "electorVote";

        /// <summary>
        /// Générer automatiquement quand l'authentification se passe bien.
        /// </summary>
        public static string keyPrefernce_ElectorRandomValue = "electorRandomValue";

    }
}
