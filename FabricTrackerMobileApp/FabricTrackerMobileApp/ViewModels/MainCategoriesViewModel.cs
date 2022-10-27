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

        public ObservableCollection<SubCategoryViewModel> SubCategoryItems { get; set; }

        public MainCategoryViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private async Task NavigateToItem(MainCategoryViewModel item)
        {
            if(item == null)
            {
                return;
            }

            var mainCategoryItemView = Resolver.Resolve<MainCategoryItemView>();

            var vm = mainCategoryItemView.BindingContext as MainCategoryItemViewModel;

            vm.MainCategoryItem = item.MainCategoryItem;

            var subCategoryItems = await repository.GetSubCategories(item.MainCategoryItem.MainCategoryId);

            if(subCategoryItems.Count > 0)
            {
                var subCategoryItemViewModels = subCategoryItems.Select(sc => CreateSubCategoryItemViewModel(sc));
                vm.SubCategoryItems = new ObservableCollection<SubCategoryViewModel>(subCategoryItemViewModels);
            }

            await Navigation.PushAsync(mainCategoryItemView);
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
        public ICommand AddItem => new Command(async () =>
        {
            var mainCategoryItemView = Resolver.Resolve<MainCategoryItemView>();
            await Navigation.PushAsync(mainCategoryItemView);
        });

        public MainCategoriesViewModel(Repository repository)
        {
            repository.OnMainCategoryItemAdded += (sender, item) => MainCategoryItems.Add(CreateMainCategoryItemViewModel(item));
            repository.OnMainCategoryItemAdded += (sender, item) => MainCategoryItems = new ObservableCollection<MainCategoryViewModel>(MainCategoryItems.OrderBy(x => x.MainCategoryItem.MainCategoryName));

            repository.OnMainCategoryItemUpdated += (sender, item) => Task.Run(async () => await LoadData());
            this.repository = repository;
            Task.Run(async () => await LoadData());
            
        }

        private async Task LoadData()
        { 
            var mainCategories = await repository.GetMainCategories();
            var mainCategoriesViewModels = mainCategories.Select(mc => CreateMainCategoryItemViewModel(mc));
            MainCategoryItems = new ObservableCollection<MainCategoryViewModel>(mainCategoriesViewModels);

            
        }

        private MainCategoryViewModel CreateMainCategoryItemViewModel(MainCategory mainCategory)
        {
            var itemViewModel = new MainCategoryViewModel(mainCategory, repository);
            itemViewModel.MainCategoryItemStatusChanged += MainCategoryItemStatusChanged;
            return itemViewModel;
        }

        private void MainCategoryItemStatusChanged(object sender, EventArgs e)
        {

        }

        
    }
}

