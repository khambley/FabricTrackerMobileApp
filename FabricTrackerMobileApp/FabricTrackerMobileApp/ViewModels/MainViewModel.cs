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
        }

        public ICommand NavigateToMainCategoriesPage => new Command(async () =>
        {
            var mainCategoriesPage = Resolver.Resolve<MainCategoriesPage>();
            await Navigation.PushAsync(mainCategoriesPage);
        });

        public ICommand NavigateToFabricsPage => new Command(async () =>
        {
            var fabricsPage = Resolver.Resolve<FabricsPage>();
            await Navigation.PushAsync(fabricsPage);
        });

     
    }
}

