using System;
using System.Threading.Tasks;
using FabricTrackerMobileApp.Data;

namespace FabricTrackerMobileApp.ViewModels
{
    public class FabricsViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public FabricsViewModel(Repository repository)
        {
            this.repository = repository;
            Task.Run(async () => await LoadData());
        }

        private async Task LoadData()
        {

        }
    }
}

