using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace votingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BehaviorDemo : ContentPage
    {
        public BehaviorDemo()
        {
            InitializeComponent();
            GoToState(true);
        }

        private void GoToState(bool isValid)
        {
            string visualState = isValid ? "Valid" : "InValid";
            VisualStateManager.GoToState(MainLayout, visualState);
        }

        private void EntryPassWord_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isValid = Regex.IsMatch(e.NewTextValue, @"[A-Za-z0-9]{6,}");
            GoToState(isValid);
            Console.WriteLine("Sa marche");
        }
    }
}