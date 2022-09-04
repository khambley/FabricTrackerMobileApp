using System;
using FabricTrackerMobileApp.Models;

namespace FabricTrackerMobileApp.ViewModels
{
    public class MainCategoryViewModel: ViewModelBase
    {
        public MainCategoryViewModel(MainCategory mainCategory)
        {
            MainCategoryItem = mainCategory;
        }
        public event EventHandler MainCategoryItemStatusChanged;

        public MainCategory MainCategoryItem { get; private set; }

    }
}

