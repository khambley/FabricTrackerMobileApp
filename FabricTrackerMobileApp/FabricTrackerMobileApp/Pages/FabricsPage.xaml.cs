using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FabricTrackerMobileApp.ViewModels;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Pages
{
    public partial class FabricsPage : ContentPage
    {
        public FabricsPage(FabricsViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;

            ItemsListView.ItemSelected += (s, e) => ItemsListView.SelectedItem = null;
        }

        void mcPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            (BindingContext as FabricsViewModel).OnMainCategoryChosen(sender, e);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            
        }
        
    }
}

