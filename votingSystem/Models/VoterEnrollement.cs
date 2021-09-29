using System;
using System.Collections.Generic;
using System.Text;

namespace votingSystem.Models
{
    /// <summary>
    /// Cette classe décrit les données qu'un electeur doit fournir pour s'enroller
    /// </summary>
    [Serializable]
    public class VoterEnrollement
    {
        public string cni { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birthday { get; set; }
        public string birthPlace { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }

    }
}
