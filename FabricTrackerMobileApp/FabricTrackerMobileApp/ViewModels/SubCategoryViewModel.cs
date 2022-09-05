using System;
using FabricTrackerMobileApp.Models;

namespace FabricTrackerMobileApp.ViewModels
{
    public class SubCategoryViewModel : ViewModelBase
    {
        public SubCategoryViewModel(SubCategory subCategoryItem)
        {
            SubCategoryItem = subCategoryItem;
        }

        public event EventHandler SubCategoryItemStatusChanged;

        public SubCategory SubCategoryItem { get; private set; }
        
    }
}

