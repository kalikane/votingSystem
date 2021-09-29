using System;
using System.Collections.Generic;
using System.Text;

namespace votingSystem.Models
{

    /// <summary>
    /// Cette classe décrit la struture de toute les réponses venant de l'API.
    /// </summary>
    public class ApiReponseData<T>
    {
        public string status { get; set; }
        
        public T data { get; set; }
        public string message { get; set; }
        public string state { get; set; }
    }

}

