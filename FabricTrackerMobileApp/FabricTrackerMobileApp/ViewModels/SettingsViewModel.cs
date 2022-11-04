using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public SettingsViewModel(Repository repository)
        {
            this.repository = repository;
        }

        public ICommand NavigateToDataBackupRestorePage => new Command(async () =>
        {
            var dataBackupRestorePage = Resolver.Resolve<DataBackupRestorePage>();
            await Navigation.PushAsync(dataBackupRestorePage);
        });

    }
}

