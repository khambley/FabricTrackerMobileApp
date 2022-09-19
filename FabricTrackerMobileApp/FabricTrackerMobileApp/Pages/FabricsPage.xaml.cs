using System;
using System.Collections.Generic;
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
        }
    }
}

