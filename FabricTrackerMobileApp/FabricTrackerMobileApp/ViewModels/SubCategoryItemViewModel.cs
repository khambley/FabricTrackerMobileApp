using System;
using FabricTrackerMobileApp.Data;

namespace FabricTrackerMobileApp.ViewModels
{
    public class SubCategoryItemViewModel :ViewModelBase 
    {
        private readonly Repository repository;

        public SubCategoryItemViewModel(Repository repository)
        {
            this.repository = repository;
        }
    }
}

