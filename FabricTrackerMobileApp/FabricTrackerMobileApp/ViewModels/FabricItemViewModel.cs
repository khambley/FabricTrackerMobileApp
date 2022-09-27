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

        public List<SubCategory> SubCategoriesList { get; set; }

        public FabricItemViewModel(Repository repository)
        {
            this.repository = repository;
            MainCategoriesList = GetMainCategoriesList();
            FabricItem = new Fabric();
        }

        public MainCategory SelectedMainCategory { get; set; }

        public SubCategory SelectedSubCategory { get; set; }

        public ICommand Save => new Command(async () =>
        {
            FabricItem.MainCategoryId = SelectedMainCategory.MainCategoryId;
            FabricItem.SubCategoryId = SelectedSubCategory.SubCategoryId;
            await repository.AddOrUpdate(FabricItem);
            await Navigation.PopAsync();
        });

        private List<MainCategory> GetMainCategoriesList()
        {
            var items = Task.Run(async () => await repository.GetMainCategories());
            return items.Result;
        }

        private List<SubCategory> GetSubCategoriesList(int mainCategoryId)
        {
            var items = Task.Run(async () => await repository.GetSubCategories(mainCategoryId));
            return items.Result;
        }

        public void OnMainCategoryChosen(object sender, EventArgs args)
        {
            var mainCategoryId = SelectedMainCategory.MainCategoryId;
            SubCategoriesList = GetSubCategoriesList(mainCategoryId);
        }
    }
}

