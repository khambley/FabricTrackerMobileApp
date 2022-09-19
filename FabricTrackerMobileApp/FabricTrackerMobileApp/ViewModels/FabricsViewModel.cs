using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Pages;
using Xamarin.Forms;

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
        public ICommand AddItem => new Command(async () =>
        {
            var fabricItemView = Resolver.Resolve<FabricItemView>();
            await Navigation.PushAsync(fabricItemView);
        });

        private async Task LoadData()
        {

        }
    }
}

