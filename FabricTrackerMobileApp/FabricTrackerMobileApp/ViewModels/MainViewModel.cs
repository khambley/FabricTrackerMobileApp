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
    public class MainViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public MainViewModel(Repository repository)
        {
            this.repository = repository;
        }

        public ICommand NavigateToMainCategoriesPage => new Command(async () =>
        {
            var mainCategoriesPage = Resolver.Resolve<MainCategoriesPage>();
            await Navigation.PushAsync(mainCategoriesPage);
        });

        public ICommand NavigateToFabricsPage => new Command(async () =>
        {
            var fabricsPage = Resolver.Resolve<FabricsPage>();
            await Navigation.PushAsync(fabricsPage);
        });

        public ICommand BackupDataCommand => new Command(async () =>
        {
            await CompressAndExportFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        });

        public async Task CompressAndExportFolder(string folderPath)
        {
            // Get a temp cache directory
            var exportTempDirectory = Path.Combine(FileSystem.CacheDirectory, "Export");

            // Delete existing exports, if any
            try
            {
                if (Directory.Exists(exportTempDirectory))
                {
                    Directory.Delete(exportTempDirectory, true);
                }
            }
            catch (Exception ex)
            {
                // Log errors
                Debug.WriteLine(ex);
            }

            // Get a timestamped filename
            var exportFilename = $"FabricTrackerAppData_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.zip";

            Directory.CreateDirectory(exportTempDirectory);

            var exportFilePath = Path.Combine(exportTempDirectory, exportFilename);

            if (File.Exists(exportFilePath))
            {
                File.Delete(exportFilePath);
            }

            // Zip everything up
            ZipFile.CreateFromDirectory(folderPath, exportFilePath, CompressionLevel.Fastest, true);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "My FabricTracker App Data",
                File = new ShareFile(exportFilePath)
            });
        }
     
    }
}

