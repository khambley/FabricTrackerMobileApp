using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Pages
{
    public partial class DataBackupRestorePage : ContentPage
    {
        public DataBackupRestorePage(DataBackupRestoreViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
        public ICommand BackupDataCommand => new Command(async () =>
        {
            await ZipAndExportDbAndImages(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        });

        public async Task ZipAndExportDbAndImages(string filePath)
        {
            var tempDirectory = Path.Combine(FileSystem.CacheDirectory, "Export");

            try
            {
                if (Directory.Exists(tempDirectory))
                    Directory.Delete(tempDirectory);
            }
            catch (Exception ex)
            {
                // Log errors
                Debug.WriteLine(ex);

            }
            var exportFilename = $"FabricTrackerAppDb_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.zip";

            Directory.CreateDirectory(tempDirectory);


            var exportDbFilePath = Path.Combine(tempDirectory, exportFilename);

            // For testing only - to see where db is exported

            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    App.Current.MainPage.DisplayAlert("Export Path", $"Db saved in {exportFilePath}", "OK");

            //});

            ZipFile.CreateFromDirectory(filePath, exportDbFilePath, CompressionLevel.NoCompression, true);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "MyFabricTracker App Data",
                File = new ShareFile(exportDbFilePath)
            });

        }
    }
}

