using System;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace FabricTrackerMobileApp.ViewModels
{
    
    public class MainCategoryItemViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public MainCategory MainCategoryItem { get; set; }

        public ObservableCollection<SubCategoryViewModel> SubCategoryItems { get; set; }

        public MainCategoryItemViewModel(Repository repository)
        {
            repository.OnSubCategoryItemAdded += (sender, item) => SubCategoryItems.Add(CreateSubCategoryItemViewModel(item));

            repository.OnSubCategoryItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            this.repository = repository;
            MainCategoryItem = new MainCategory();
            Task.Run(async () => await LoadData());
        }

        public ICommand Save => new Command(async () =>
        {
            await repository.AddOrUpdateMainCategory(MainCategoryItem);
            await Navigation.PopAsync();
        });
        public ICommand AddSubCategoryItem => new Command(async () =>
        {
            
        });
        private async Task LoadData()
        {
            var subCategoryItems = await repository.GetSubCategories(MainCategoryItem.MainCategoryId);
            var subCategoryItemViewModels = subCategoryItems.Select(sc => CreateSubCategoryItemViewModel(sc));
            SubCategoryItems = new ObservableCollection<SubCategoryViewModel>(subCategoryItemViewModels);
        }

        private SubCategoryViewModel CreateSubCategoryItemViewModel(SubCategory subCategoryItem)
        {
            var subCategoryItemViewModel = new SubCategoryViewModel(subCategoryItem);
            subCategoryItemViewModel.SubCategoryItemStatusChanged += SubCategoryItemStatusChanged;
            return subCategoryItemViewModel;
        }

        private void SubCategoryItemStatusChanged(object sender, EventArgs e)
        {

        }
        
    }
}

