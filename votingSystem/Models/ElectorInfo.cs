using System;
using System.Collections.Generic;
using System.Text;

namespace votingSystem.Models
{
    public class ElectorInfo
    {
        /// <summary>
        /// Token que l'électeur utilisera pour voter.
        /// </summary>
        public string accessToken { get; set; }

        /// <summary>
        /// Information de l'électeur chiffré.
        /// </summary>
        public string electorInfos { get; set; }
    }
}
