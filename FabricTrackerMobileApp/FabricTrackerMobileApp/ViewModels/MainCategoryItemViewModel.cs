using System;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.ViewModels
{
    
    public class MainCategoryItemViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public MainCategory MainCategoryItem { get; set; }

        public MainCategoryItemViewModel(Repository repository)
        {
            this.repository = repository;
            MainCategoryItem = new MainCategory();
        }

        public ICommand Save => new Command(async () =>
        {
            await repository.AddOrUpdateMainCategory(MainCategoryItem);
            await Navigation.PopAsync();
        });
        
    }
}

