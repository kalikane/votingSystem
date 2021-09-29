using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using votingSystem.Helpers;
using votingSystem.Models;
using votingSystem.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace votingSystem.ViewModels
{
    public class BulletinViewModel :  BaseViewModel
    {

        public ObservableCollection<BulletinVote> BulletinsDeVote { get; set; }
        public Command votingCommand { get; set; }


        public BulletinViewModel()
        {

            BulletinsDeVote = new ObservableCollection<BulletinVote>();
            votingCommand = new Command(async() => await VotingCommandAsyn());
            //Récupération des bulletins de vote
            _ = GetBulletinsVote();

        }

        private Task VotingCommandAsyn()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Appel le service pour récupérer les bulletins de votes de l'électeur.
        /// </summary>
        /// <returns></returns>
        public  async Task GetBulletinsVote()
        {
            string rs = Preferences.Get(Constante.keyPrefernce_ElectorRandomValue, string.Empty);
            string elc = Preferences.Get(Constante.keyPreference_ElectorLockCode, string.Empty);

            VotingService vs = new VotingService();

            try
            {
                var bv = await vs.GetBulletinsVote(rs, elc);
                BulletinsDeVote.Clear();

                foreach (var b in bv)
                {
                    BulletinsDeVote.Add(b);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"BulletinViewModel:GetBulletinsVote() => Erreur survenu: {ex}");
            }

            
        }
    }
}
