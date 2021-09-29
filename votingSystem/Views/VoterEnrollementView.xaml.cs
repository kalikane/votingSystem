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
    public partial class VoterEnrollementView : ContentPage
    {
        public VoterEnrollementView()
        {
            InitializeComponent();
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            
        }
    }
}