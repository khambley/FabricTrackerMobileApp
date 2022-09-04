using System;
using System.Threading.Tasks;
using FabricTrackerMobileApp.Data;
using System.Windows.Input;
using FabricTrackerMobileApp.Pages;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using FabricTrackerMobileApp.Models;

namespace FabricTrackerMobileApp.ViewModels
{
    public class MainCategoriesViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public ObservableCollection<MainCategoryViewModel> MainCategoryItems { get; set; }

        public ICommand AddItem => new Command(async () =>
        {
            var mainCategoryItemView = Resolver.Resolve<MainCategoryItemView>();
            await Navigation.PushAsync(mainCategoryItemView);
        });

        public MainCategoriesViewModel(Repository repository)
        {
            this.repository = repository;
            
        }

        private async Task LoadData()
        {
            var mainCategories = await repository.GetMainCategories();
            var mainCategoriesViewModels = mainCategories.Select(mc => CreateMainCategoryItemViewModel(mc));
            MainCategoryItems = new ObservableCollection<MainCategoryViewModel>(mainCategoriesViewModels);
        }

        private MainCategoryViewModel CreateMainCategoryItemViewModel(MainCategory mainCategory)
        {
            var itemViewModel = new MainCategoryViewModel(mainCategory);
            itemViewModel.MainCategoryItemStatusChanged += MainCategoryItemStatusChanged;
            return itemViewModel;
        }

        private void MainCategoryItemStatusChanged(object sender, EventArgs e)
        {

        }

        
    }
}

