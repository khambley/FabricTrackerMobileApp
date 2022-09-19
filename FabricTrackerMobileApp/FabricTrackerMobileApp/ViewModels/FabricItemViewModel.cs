using System;
using FabricTrackerMobileApp.Data;

namespace FabricTrackerMobileApp.ViewModels
{
    public class FabricItemViewModel : ViewModelBase
    {
        private readonly Repository repository;
        public FabricItemViewModel(Repository repository)
        {
            this.repository = repository;
        }
    }
}

