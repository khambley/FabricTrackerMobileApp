using System;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using FabricTrackerMobileApp.Pages;
using Autofac;
using System.Data.Common;

namespace FabricTrackerMobileApp.ViewModels
{
    
    public class MainCategoryItemViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public MainCategory MainCategoryItem { get; set; }

        public int MainCategoryId { get; set; }

        public ObservableCollection<SubCategoryViewModel> SubCategoryItems { get; set; }

        public MainCategoryItemViewModel(Repository repository)
        {
            repository.OnSubCategoryItemAdded += (sender, item) => SubCategoryItems.Add(CreateSubCategoryItemViewModel(item));
            repository.OnSubCategoryItemAdded += (sender, item) => SubCategoryItems = new ObservableCollection<SubCategoryViewModel>(SubCategoryItems.OrderBy(x => x.SubCategoryItem.SubCategoryName));
            repository.OnSubCategoryItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            this.repository = repository;
            MainCategoryItem = new MainCategory();

            Task.Run(async () => await LoadData());
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            await Save();
            
        });

        public ICommand AddSubCategoryItem => new Command(async () =>
        {
            if(MainCategoryItem.MainCategoryId <= 0)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", "You must save the main category first before adding a subcategory.", "Go Back");
                });
                
            }
            else
            {
                var subCategoryItemView = Resolver.Resolve<SubCategoryItemView>("mainCategoryItem", MainCategoryItem);
                await Navigation.PushAsync(subCategoryItemView);
            }
            
        });

        public async Task Save()
        {
            if (MainCategoryItem.MainCategoryName != null)
            {
                await repository.AddOrUpdateMainCategory(MainCategoryItem);
                //await Navigation.PopAsync();
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", "A MainCategory is required. Add an entry or press the < Back button in the navbar to cancel.", "Go Back");
                });
            }
        }
        private async Task LoadData()
        {
            var subCategoryItems = await repository.GetSubCategories(MainCategoryItem.MainCategoryId);
            var subCategoryItemViewModels = subCategoryItems.Select(sc => CreateSubCategoryItemViewModel(sc));
            SubCategoryItems = new ObservableCollection<SubCategoryViewModel>(subCategoryItemViewModels);
        }

        private SubCategoryViewModel CreateSubCategoryItemViewModel(SubCategory subCategoryItem)
        {
            var subCategoryItemViewModel = new SubCategoryViewModel(subCategoryItem, repository);
            subCategoryItemViewModel.SubCategoryItemStatusChanged += SubCategoryItemStatusChanged;
            return subCategoryItemViewModel;
        }

        private void SubCategoryItemStatusChanged(object sender, EventArgs e)
        {

        }
        
    }
}

