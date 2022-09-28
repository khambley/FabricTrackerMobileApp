using System;
using System.Collections.Generic;
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

        void mcPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            (BindingContext as FabricItemViewModel).OnMainCategoryChosen(sender, e);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as FabricItemViewModel).MainCategoriesList = (BindingContext as FabricItemViewModel).GetMainCategoriesList();
            mcPicker.SelectedItem = null;
            (BindingContext as FabricItemViewModel).SubCategoriesList = (BindingContext as FabricItemViewModel).GetSubCategoriesList();
            scPicker.SelectedItem = null;
        }
    }
}

