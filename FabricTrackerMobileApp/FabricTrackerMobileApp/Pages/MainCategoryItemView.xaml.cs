using System;
using System.Collections.Generic;
using FabricTrackerMobileApp.ViewModels;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Pages
{
    public partial class MainCategoryItemView : ContentPage
    {
        public MainCategoryItemView(MainCategoryItemViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
    }
}

