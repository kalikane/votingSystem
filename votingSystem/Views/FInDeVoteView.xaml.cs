using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace votingSystem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FInDeVoteView : ContentPage
    {
        public FInDeVoteView()
        {
            InitializeComponent();
        }

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new VoterEnrollementView());

        }
    }
}