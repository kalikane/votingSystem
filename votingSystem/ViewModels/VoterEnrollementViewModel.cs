using System;
using System.Collections.Generic;
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
    public class VoterEnrollementViewModel : BaseViewModel
    {
        public string ScanMessage { get; set; }

        private bool tokenRecuperer;

        public bool TokenRecuperer
        {
            get { return tokenRecuperer; }
            set { tokenRecuperer = value; OnPropertyChanged(); }
        }

        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; OnPropertyChanged(); }
        }


        private string _CNI =Guid.NewGuid().ToString();

        public string CNI
        {
            get { return _CNI; }
            set
            {
                _CNI = value;
                OnPropertyChanged();
            }
        }

        private string _name  ="Tchoumi kane";

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _surname = "Chalers wadel";

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        private DateTime _birthDay = DateTime.Now;

        public DateTime BirthDay
        {
            get { return _birthDay; }
            set
            {
                _birthDay = value;
                OnPropertyChanged();
            }
        }

        private string _birthPlace ="Yaoundé";

        public string BirthPlace
        {
            get { return _birthPlace; }
            set
            {
                _birthPlace = value;
                OnPropertyChanged();
            }
        }

        private string _fatherName ="Tchoumi Jacques";

        public string FatherName
        {
            get { return _fatherName; }
            set
            {
                _fatherName = value;
                OnPropertyChanged();
            }
        }

        private string _motherName = "Mendo Myriam";

        public string MotherName
        {
            get { return _motherName; }
            set
            {
                _motherName = value;
                OnPropertyChanged();
            }
        }

        public Command EnrollementCommand { get; set; }

        public Command SetTokenBoolCommand { get; set; }


        public VoterEnrollementViewModel()
        {
            ScanMessage = "Veuiller scanner votre QRCode pour accedr au sytème";
            IsVisible = false;
            TokenRecuperer = false;
            EnrollementCommand = new Command(async () => await EnrollementCommandAsync());
            SetTokenBoolCommand = new Command(async () => await SetTokenBoolCommandAsync(false));

        }

        public async Task SetTokenBoolCommandAsync(bool Tokenstate)
        {
            TokenRecuperer = Tokenstate;
        }

        public async Task EnrollementCommandAsync()
        {
            Console.WriteLine($"Token: {Preferences.Get(Constante.keyPreference_AccesToken, string.Empty)}");

            ScanMessage = "Veuillez patienter SVP !!!";
            await Application.Current.MainPage.Navigation.PushAsync(new VoterAuthentificationView());

         


            /*
            if (string.IsNullOrEmpty(CNI) || string.IsNullOrEmpty(Name)
                || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname)
                || string.IsNullOrEmpty(BirthDay.ToString("d"))
                || string.IsNullOrEmpty(BirthPlace)
                || string.IsNullOrEmpty(FatherName)
                || string.IsNullOrEmpty(MotherName))
            {
                await Application.Current.MainPage.DisplayAlert("Champs vides", "Veuillez remplir tout les champs svp", "Ok");
                return;
            }
            //Construire l'objet service
            VotingService vrs = new VotingService();

            // Construction de l'objet
            var voter = new VoterEnrollement
            {
                cni = CNI,
                name = Name,
                surname = Surname,
                birthday = BirthDay.ToString("d"),
                birthPlace = BirthPlace,
                fatherName = FatherName,
                motherName = MotherName
            };

            if (await vrs.EnrollVoterAsync(voter))
            {

                string message = $"Electeur inscrit avec token {Preferences.Get(Constante.keyPreference_AccesToken, "")} ";
                await Application.Current.MainPage.DisplayAlert("Inscription", message, "OK");

                // On navigue vers la page suivante.
                await Application.Current.MainPage.Navigation.PushAsync(new VoterAuthentificationView());

            } 

            */
            // On navigue vers la page suivante.

        }




    }
}
