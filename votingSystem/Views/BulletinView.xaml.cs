using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingSystem.Models;
using votingSystem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace votingSystem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BulletinView : ContentPage
    {
        public BulletinViewModel bvvm;
        public BulletinView()
        {
            InitializeComponent();
            bvvm = new BulletinViewModel();
            BindingContext = bvvm;

        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bv = e.CurrentSelection.FirstOrDefault() as BulletinVote;
            if (bv == null)
                return;

            // requete de vote
            bvvm.VotingCommandAsyn(bv);

            Console.WriteLine($"Bulletin: {bv.partyCode}");


            ((CollectionView)sender).SelectedItem = null;
        }
    }
}