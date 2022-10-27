using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Pages;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using FabricTrackerMobileApp.Models;
using System.IO;

namespace FabricTrackerMobileApp.ViewModels
{
    public class FabricsViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public ObservableCollection<FabricViewModel> Items { get; set; }

        public bool ShowAll { get; set; }

        public ObservableCollection<MainCategory> MainCategoriesList { get; set; }

        public ObservableCollection<SubCategory> SubCategoriesList { get; set; }

        public MainCategory SelectedMainCategory { get; set; }

        public SubCategory SelectedSubCategory { get; set; }

        public FabricViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToFabricItem(value));
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public bool NoItemsToDisplayLabel { get; set; }

        private async Task NavigateToFabricItem(FabricViewModel item)
        {
            if (item == null)
            {
                return;
            }
            var itemView = Resolver.Resolve<FabricItemView>("selectedMainCategory", repository.GetMainCategoryById(item.Item.MainCategoryId));
            var viewModel = itemView.BindingContext as FabricItemViewModel;

            viewModel.FabricItem = item.Item;
            viewModel.ImageSource = item.LoadPhoto();
            //viewModel.SelectedMainCategory = repository.GetMainCategoryById(item.Item.MainCategoryId);
            
            await Navigation.PushAsync(itemView);
        }

        public FabricsViewModel(Repository repository)
        {
            repository.OnItemAdded += (sender, item) => Items.Add(CreateFabricViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            this.repository = repository;
            ShowAll = true;
            NoItemsToDisplayLabel = false;
            Task.Run(async () => await LoadData());
        }

        public ICommand AddItem => new Command(async () =>
        {
            var fabricItemView = Resolver.Resolve<FabricItemView>();
            await Navigation.PushAsync(fabricItemView);
        });

        public ICommand FilterByCategoryCommand => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });

        public ICommand ClearFilterCommand => new Command(async () =>
        {
            ShowAll = true;
            NoItemsToDisplayLabel = false;
            await LoadData();
        });

        private async Task LoadData()
        {
            var items = await repository.GetFabrics();

            if (SelectedMainCategory == null)
            {
                MainCategoriesList = new ObservableCollection<MainCategory>(repository.GetMainCategories().Result);
            }

            if (!ShowAll)
            {
                items = items.Where(x => x.MainCategoryName == SelectedMainCategory.MainCategoryName).ToList();

                if(SelectedSubCategory != null)
                {
                    items = items.Where(x => x.MainCategoryName == SelectedMainCategory.MainCategoryName && x.SubCategoryName == SelectedSubCategory.SubCategoryName).ToList();
                }
                if(items.Count == 0)
                {
                    NoItemsToDisplayLabel = true;
                }
            }

            var itemViewModels = items.Select(i => CreateFabricViewModel(i));
            Items = new ObservableCollection<FabricViewModel>(itemViewModels);
        }
        private FabricViewModel CreateFabricViewModel(Fabric item)
        {
            var itemViewModel = new FabricViewModel(item, repository);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }
        private void ItemStatusChanged(object sender, EventArgs e)
        {

        }

        public void OnMainCategoryChosen(object sender, EventArgs args)
        {
            if (SelectedMainCategory != null)
            {
                var mainCategoryId = SelectedMainCategory.MainCategoryId;
                SubCategoriesList = GetSubCategoriesList(mainCategoryId);
            }
        }
        public ObservableCollection<SubCategory> GetSubCategoriesList(int mainCategoryId = 0)
        {
            var items = Task.Run(async () => await repository.GetSubCategories(mainCategoryId));
            return new ObservableCollection<SubCategory>(items.Result);
        }

    }
}

