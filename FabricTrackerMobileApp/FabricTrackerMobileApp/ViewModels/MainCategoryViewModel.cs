using System;
using System.Windows.Input;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Models;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.ViewModels
{
    public class MainCategoryViewModel: ViewModelBase
    {
        private readonly Repository repository;
        public MainCategoryViewModel(MainCategory mainCategory, Repository repository)
        {
            this.repository = repository;
            MainCategoryItem = mainCategory;
        }
        public event EventHandler MainCategoryItemStatusChanged;

        public MainCategory MainCategoryItem { get; private set; }

        public ICommand DeleteMainCategory => new Command(async () =>
        {
            await repository.DeleteMainCategory(MainCategoryItem);

        });

    }
}

