using System;
using System.Collections.Generic;
using FabricTrackerMobileApp.ViewModels;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Pages
{
    public partial class SubCategoryItemView : ContentPage
    {
        public SubCategoryItemView(SubCategoryItemViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
    }
}

