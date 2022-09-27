using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Models;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.ViewModels
{
    public class FabricItemViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public Fabric FabricItem { get; set; }

        public List<MainCategory> MainCategoriesList { get; set; }

        public FabricItemViewModel(Repository repository)
        {
            this.repository = repository;
            MainCategoriesList = GetCategoriesList();
            FabricItem = new Fabric();
        }

        public MainCategory SelectedMainCategory { get; set; }

        public SubCategory SelectedSubCategory { get; set; }

        public ICommand Save => new Command(async () =>
        {
            await repository.AddOrUpdate(FabricItem);
            await Navigation.PopAsync();
        });

        private List<MainCategory> GetCategoriesList()
        {
            var items = Task.Run(async () => await repository.GetMainCategories());
            return items.Result;
        }
    }
}

