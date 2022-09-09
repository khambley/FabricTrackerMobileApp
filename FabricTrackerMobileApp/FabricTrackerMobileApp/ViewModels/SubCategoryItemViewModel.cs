using System;
using FabricTrackerMobileApp.Data;
using System.Windows.Input;
using FabricTrackerMobileApp.Models;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.ViewModels
{
    public class SubCategoryItemViewModel : ViewModelBase 
    {
        private readonly Repository repository;

        public SubCategory SubCategoryItem { get; set; }

        public int MainCategoryId { get; set; }

        public MainCategory MainCategoryItem { get; set; }

        public SubCategoryItemViewModel(Repository repository)
        {
            this.repository = repository;
            SubCategoryItem = new SubCategory();
            SubCategoryItem.MainCategoryId = MainCategoryId;
        }
        public ICommand Save => new Command(async () =>
        {
            SubCategoryItem.MainCategoryId = MainCategoryId;
            await repository.AddSubCategory(SubCategoryItem);
            await Navigation.PopAsync();
        });
        public ICommand DeleteSubCategory => new Command(async () =>
        {
            //bool answer = await page.DisplayAlert($"Delete Subcategory", "Are you sure you want to delete {SubCategoryItem}", "OK", "Cancel");
            await repository.DeleteSubCategory(SubCategoryItem);
            

        });
    }
}

