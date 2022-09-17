using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Pages;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Repository repository;

        

        public MainViewModel(Repository repository)
        {
            this.repository = repository;
            Task.Run(async () => await LoadData());
        }

        //public ICommand NavigateToViewFabricsPage => new Command(async () =>
        //{
        //    await Navigation.PushAsync(new ViewFabricsPage());
        //});

        public ICommand NavigateToMainCategoriesPage => new Command(async () =>
        {
            var mainCategoriesPage = Resolver.Resolve<MainCategoriesPage>();
            await Navigation.PushAsync(mainCategoriesPage);
        });

        private async Task LoadData()
        {

        }
    }
}

