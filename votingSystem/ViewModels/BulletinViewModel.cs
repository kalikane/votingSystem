using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using votingSystem.Helpers;
using votingSystem.Models;
using votingSystem.Services;
using votingSystem.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace votingSystem.ViewModels
{
    public class BulletinViewModel : BaseViewModel
    {

        public ObservableCollection<BulletinVote> BulletinsDeVote { get; set; }
        public Command votingCommand { get; set; }


        public BulletinViewModel()
        {
            BulletinsDeVote = new ObservableCollection<BulletinVote>();
            //Récupération des bulletins de vote
            _ = GetBulletinsVote();
        }

        /// <summary>
        /// Lance l'opération de vote.
        /// </summary>
        /// <param name="bulletinSelect"></param>
        /// <returns></returns>
        public async Task VotingCommandAsyn(BulletinVote bulletinSelect)
        {
            if (bulletinSelect == null) return;

            //Pop de confirmation 
            bool confirm = await Application.Current.MainPage.DisplayAlert("CONFIRMATION", "Confirmez-vous votre vote ?", "Yes", "No");
            VotingService vs = new VotingService();

            string lockCode = Preferences.Get(Constante.keyPreference_ElectorLockCode, string.Empty);
            string randomCode = Preferences.Get(Constante.keyPrefernce_ElectorRandomValue, string.Empty);
            string candidatChoice = bulletinSelect.encryptedBallot;


            if (confirm)
            {
                await vs.VoteAsync(candidatChoice, lockCode, randomCode);

                await Application.Current.MainPage.DisplayAlert("CONFIRMATION", "Merci, Votre vote a bien été enrégistré", "OK");

                // Allez sur le HUD FinDeVote
                await Application.Current.MainPage.Navigation.PushModalAsync(new FInDeVoteView());


            }
            else
            {
                return;
            }

        }

        /// <summary>
        /// Appel le service pour récupérer les bulletins de votes de l'électeur.
        /// </summary>
        /// <returns></returns>
        public async Task GetBulletinsVote()
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
