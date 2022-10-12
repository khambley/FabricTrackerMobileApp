using System;
using System.Collections.Generic;
using FabricTrackerMobileApp.Models;
using FabricTrackerMobileApp.ViewModels;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Pages
{
    public partial class FabricItemView : ContentPage
    {
        public FabricItemView(MainCategory selectedMainCategory, FabricItemViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;

            mcPicker.SelectedItem = selectedMainCategory;
        }

        void mcPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            (BindingContext as FabricItemViewModel).OnMainCategoryChosen(sender, e);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as FabricItemViewModel).MainCategoriesList = (BindingContext as FabricItemViewModel).GetMainCategoriesList();
            //mcPicker.SelectedItem = (BindingContext as FabricItemViewModel).SelectedMainCategory;
            (BindingContext as FabricItemViewModel).SubCategoriesList = (BindingContext as FabricItemViewModel).GetSubCategoriesList();
            //scPicker.SelectedItem = null;
        }
    }
}

