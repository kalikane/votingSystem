using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingSystem.Helpers;
using votingSystem.Services;
using votingSystem.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace votingSystem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoterEnrollementView : ContentPage
    {
        public VoterEnrollementViewModel vevm;
        public VoterEnrollementView()
        {
            InitializeComponent();
            vevm = new VoterEnrollementViewModel();
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void Handle_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
           {
               //vevm.SetTokenBoolCommandAsync(true);
               //_TokenText.Text = $"Token: {result.Text} type: {result.BarcodeFormat.ToString()}";
               //Console.WriteLine(result.Text);
               //_scanView.IsVisible = false;

               await DisplayAlert("Scanned result", result.Text, "OK");
           });
        }


        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            //try
            //{
            //    var scanner = DependencyService.Get<IQrScanningService>();
            //    var result = await scanner.ScanAsync();
            //    if (result != null)
            //    {
            //        Console.WriteLine($"Le result: {result}");
            //        IsVisible = false;
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}



            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //Sauvegarde le token
                    //Ouvrir la page d'authentification
                    await Navigation.PopAsync();
                    string token = result.Text;
                    bool continuer = await DisplayAlert("Recupération Token", "Super votre token a bien été récupéré : " + token, "Continuer", "Annuler");


                    if (continuer)
                    {
                        Preferences.Set(Constante.keyPreference_AccesToken, token);

                        //Vérification du token
                        vevm.EnrollementCommandAsync();

                    }
                });
            };
        }
    }
}