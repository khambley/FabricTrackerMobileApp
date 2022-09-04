using System;
using System.Collections.Generic;
using FabricTrackerMobileApp.ViewModels;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Pages
{
    public partial class MainCategoriesPage : ContentPage
    {
        public MainCategoriesPage(MainCategoriesViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
    }
}

