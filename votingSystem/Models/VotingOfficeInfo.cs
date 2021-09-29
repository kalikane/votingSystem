using System;
using System.Collections.Generic;
using System.Text;

namespace votingSystem.Models
{
    /// <summary>
    /// Rerpesesnte les données d'un bureau de vote.
    /// </summary>
    public class VotingOfficeInfo
    {
        public string code { get; set; }
        public string officeName { get; set; }
        public string localisation { get; set; }
    }
}
