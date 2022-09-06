using System;
using System.Collections.Generic;
using FabricTrackerMobileApp.Models;
using FabricTrackerMobileApp.ViewModels;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Pages
{
    public partial class SubCategoryItemView : ContentPage
    {
        public SubCategoryItemView(MainCategory mainCategoryItem, SubCategoryItemViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            viewModel.MainCategoryId = mainCategoryItem.MainCategoryId;
            viewModel.MainCategoryItem = mainCategoryItem;
            BindingContext = viewModel;
        }
    }
}

