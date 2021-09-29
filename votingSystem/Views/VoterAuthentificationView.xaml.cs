using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingSystem.Models;
using votingSystem.Services;
using votingSystem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace votingSystem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoterAuthentificationView : ContentPage
    {
        public VoterAuthentificationViewModel vavm;
        public VoterAuthentificationView()
        {
            InitializeComponent();
            vavm = new VoterAuthentificationViewModel();
            BindingContext = vavm;
        }

        private  void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var votingOfficeSelected = e.CurrentSelection.FirstOrDefault() as VotingOfficeInfo;
            if (votingOfficeSelected == null)
                return;

            // requete d'authentification.
            _ = vavm.AuthentificationCommandAsync(votingOfficeSelected);

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}