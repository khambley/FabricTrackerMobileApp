using System;
using System.Collections.Generic;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Models;
using FabricTrackerMobileApp.ViewModels;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Pages
{
    public partial class FabricItemView : ContentPage
    {
        public FabricItemView(FabricItemViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
   
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void mcPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            (BindingContext as FabricItemViewModel).OnMainCategoryChosen(sender, e);
        }
    }
}

