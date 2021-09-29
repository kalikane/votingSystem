using System;
using votingSystem.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace votingSystem
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new VoterEnrollementView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
