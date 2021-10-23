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
    public class VoterAuthentificationViewModel : BaseViewModel
    {

        /// <summary>
        /// Token de l'électeur qui vient de l'enrollement.
        /// </summary>
        private string accessToken;

        public string AccesToken
        {
            get { return accessToken; }
            set
            {
                accessToken = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Liste qui observe les derniers element.
        /// </summary>
        public ObservableCollection<VotingOfficeInfo> VotingOffices { get; set; }

        /// <summary>
        /// Command lancé quand le joueur click sur le bouton s'authentifier.
        /// </summary>
        public Command authentificationCommand { get; set; }


        public VoterAuthentificationViewModel()
        {
            accessToken = Preferences.Get(Constante.keyPreference_AccesToken, string.Empty);
            VotingOffices = new ObservableCollection<VotingOfficeInfo>();

            _ = GetVotingOffices();
        }

        public async Task AuthentificationCommandAsync(VotingOfficeInfo votingOffice)
        {
            if (votingOffice == null) return;


            if (string.IsNullOrEmpty(accessToken)) return;

            VotingService vs = new VotingService();
            // si l'authentification se passe bien direction BulletinView.
            if (await vs.AuthenticationVoterAsync(accessToken, votingOffice.code))
            {
                // Generation d'un electorRandomValue.
                Preferences.Set(Constante.keyPrefernce_ElectorRandomValue, new VotingService().GetRandomString());
                
                // ALlons sur la vue Bulletin de vote.
                await Application.Current.MainPage.Navigation.PushModalAsync(new BulletinView());
            }
            else
                await Application.Current.MainPage.DisplayAlert("AUTHENTIFICATION ELECTEUR", "Une erreur est survenu lors de votre authentification", "OK");

        }

        /// <summary>
        /// Récupère les Bureau de vote.
        /// </summary>
        /// <returns></returns>
        public async Task GetVotingOffices()
        {
            try
            {
                var votingOffices = await new VotingService().GetVotingOfficeAsync();

                VotingOffices.Clear();

                foreach (var item in votingOffices)
                {
                    VotingOffices.Add(item);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"erreur: {e.Message}");
            }
        }
    }
}
