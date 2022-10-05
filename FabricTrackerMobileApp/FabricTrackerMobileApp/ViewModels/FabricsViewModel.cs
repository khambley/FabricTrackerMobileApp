using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Pages;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using FabricTrackerMobileApp.Models;

namespace FabricTrackerMobileApp.ViewModels
{
    public class FabricsViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public ObservableCollection<FabricViewModel> Items { get; set; }

        public FabricsViewModel(Repository repository)
        {
            repository.OnItemAdded += (sender, item) => Items.Add(CreateFabricViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            this.repository = repository;
            Task.Run(async () => await LoadData());
        }
        public ICommand AddItem => new Command(async () =>
        {
            var fabricItemView = Resolver.Resolve<FabricItemView>();
            await Navigation.PushAsync(fabricItemView);
        });

        private async Task LoadData()
        {
            var items = await repository.GetFabrics();
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
    }
}

