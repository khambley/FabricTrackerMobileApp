using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.Data;
using Xamarin.Essentials;
using Xamarin.Forms;
using static SQLite.SQLite3;

namespace FabricTrackerMobileApp.ViewModels
{
    public class DataBackupRestoreViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public DataBackupRestoreViewModel(Repository repository)
        {
            this.repository = repository;
        }

        public ICommand BackupDataCommand => new Command(async () =>
        {
            await ZipAndExportDbAndImages(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Data"));
        });

        public ICommand RestoreDataCommand => new Command(async () =>
        {
            await RestoreDbAndImages();
        });

        public async Task RestoreDbAndImages()
        {
            var fileResult = await FilePicker.PickAsync();

            var sourcePath = fileResult.FullPath;

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Data");

            if (sourcePath != null)
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Import File", $"Would you like to import {fileResult.FileName}", "OK", "Cancel");

                if (answer)
                {
                    if (Directory.Exists(databasePath))
                    {
                        if (File.Exists(Path.Combine(databasePath, "FabricTrackerMobileApp.db")))
                        {
                            File.Delete(Path.Combine(databasePath, "FabricTrackerMobileApp.db"));
                        }

                        var imagesFilePath = Path.Combine(databasePath, "images");

                        if (Directory.Exists(imagesFilePath))
                        {
                            foreach (var file in Directory.GetFiles(imagesFilePath))
                            {
                                File.Delete(file);
                            }

                            Directory.Delete(imagesFilePath);
                        }

                        Directory.Delete(databasePath);

                        ZipFile.ExtractToDirectory(sourcePath, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                        await Navigation.PopToRootAsync();

                    }   
                } 
            }            
        }
        
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

            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Export Path", $"Db saved in {exportDbFilePath}", "OK");

            });

            ZipFile.CreateFromDirectory(filePath, exportDbFilePath, CompressionLevel.NoCompression, true);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "MyFabricTracker App Data",
                File = new ShareFile(exportDbFilePath)
            });
        }
    }
}

