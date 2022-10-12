using System;
using System.Windows.Input;
using Xamarin.Forms;
using FabricTrackerMobileApp.Models;
using FabricTrackerMobileApp.Data;

namespace FabricTrackerMobileApp.ViewModels
{
    public class SubCategoryViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public SubCategoryViewModel(SubCategory subCategoryItem, Repository repository)
        {
            SubCategoryItem = subCategoryItem;
            this.repository = repository;
        }

        public event EventHandler SubCategoryItemStatusChanged;

        public SubCategory SubCategoryItem { get; private set; }

        public ICommand DeleteSubCategory => new Command(async () =>
        {
            //bool answer = await page.DisplayAlert($"Delete Subcategory", "Are you sure you want to delete {SubCategoryItem}", "OK", "Cancel");
            //if (answer)
            //{
                
            //}
            await repository.DeleteSubCategory(SubCategoryItem);

        });

    }
}

