using System;
using System.Collections.Generic;
using System.Text;

namespace votingSystem.Models
{
    /// <summary>
    /// Structure de la réponse quand on s'attend à recevoir une liste d'objet dans le content.data de la réponse.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiReponseDatas<T>
    {
        public string status { get; set; }

        public List<T> data { get; set; }
        public string message { get; set; }
        public string state { get; set; }
    }
}
