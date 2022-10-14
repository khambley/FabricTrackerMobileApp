using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Models;
using FabricTrackerMobileApp.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.ViewModels
{
    public class FabricItemViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public Fabric FabricItem { get; set; }

        public string ImagePath { get; set; }

        public ImageSource ImageSource { get; set; }

        public byte[] ImageBytes { get; set; }

        public decimal TotalYards { get; set; }//=> FabricItem.TotalInches != null ? (decimal)FabricItem.TotalInches / 36 : 0;

        public ObservableCollection<MainCategory> MainCategoriesList { get; set; }

        public ObservableCollection<SubCategory> SubCategoriesList { get; set; }

        public FabricItemViewModel(Repository repository)
        {
            this.repository = repository;
            MainCategoriesList = GetMainCategoriesList();
            FabricItem = new Fabric();
            
        }

        public MainCategory SelectedMainCategory { get; set; }

        public SubCategory SelectedSubCategory { get; set; }

        #region Commands

        public ICommand Save => new Command(async () =>
        {
            FabricItem.MainCategoryId = SelectedMainCategory != null ? SelectedMainCategory.MainCategoryId : FabricItem.MainCategoryId;
            FabricItem.SubCategoryId = SelectedSubCategory != null ? SelectedSubCategory.SubCategoryId : FabricItem.SubCategoryId;
            await repository.AddOrUpdate(FabricItem);
            await Navigation.PopAsync();
        });

        public ICommand CalculateTotalYardsCommand => new Command(() =>
        {
            TotalYards = FabricItem.TotalInches != null ? (decimal)FabricItem.TotalInches / 36 : 0;
        });

        public ICommand CaptureImageCommand => new Command(async () =>
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            var stream = await LoadPhotoAsync(photo);
        });

        private async Task<Stream> LoadPhotoAsync(FileResult photo)
        {
            // cancelled
            if (photo == null)
                return null;

            FabricItem.ItemCode = repository.GetUniqueItemCode();

            var uniqueFileName = FabricItem.ItemCode + Path.GetExtension(photo.FileName);

            // get the image stream
            var stream = await photo.OpenReadAsync();

            // get the documents path on device
            var imageFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "images");

            // create images folder if it doesn't exist
            if (!Directory.Exists(imageFilesPath))
                Directory.CreateDirectory(imageFilesPath);

            // create image filename based on item code
            var newFile = Path.Combine(imageFilesPath, uniqueFileName);

            // save image file to local storage
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            //FabricItem.ImagePath = newFile;
            FabricItem.ImagePath = uniqueFileName;

            ImagePath = newFile;


            // To display image...
            // get image from stream
            stream = await photo.OpenReadAsync();

            //set the ImageBytes property
            using (var memoryStream = new System.IO.MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            // set the ImageSource property from the ImageBytes byte array
            ImageSource = ImageSource.FromStream(() => new MemoryStream(ImageBytes));

            return stream;
        }
        private ImageSource DisplayPhoto()
        {
            // retrieve image from ImagePath in db.
            if (FabricItem.ImagePath != null)
            {
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "images/" + FabricItem.ImagePath);

                if (File.Exists(filePath))
                {
                    ImageBytes = File.ReadAllBytes(filePath);
                    ImageSource = ImageSource.FromStream(() => new MemoryStream(ImageBytes));
                    return ImageSource;
                }
                else
                {
                    return null;
                }
            } else
            {
                return null;
            }
        }
        public ICommand ChooseImageCommand => new Command(async () =>
        {
            var photo = await MediaPicker.PickPhotoAsync();
            var stream = await LoadPhotoAsync(photo);
        });

        public ICommand AddMainCategoryCommand => new Command(async () =>
        {
            var mainCategoriesPage = Resolver.Resolve<MainCategoriesPage>();
            await Navigation.PushAsync(mainCategoriesPage);
        });

        public ICommand AddSubCategoryCommand => new Command(async () =>
        {
            var subCategoryItemView = Resolver.Resolve<SubCategoryItemView>("mainCategoryItem", SelectedMainCategory);
            await Navigation.PushAsync(subCategoryItemView);
        });
        public ObservableCollection<MainCategory> GetMainCategoriesList()
        {
            var items = Task.Run(async () => await repository.GetMainCategories());
            return new ObservableCollection<MainCategory>(items.Result);
        }
        #endregion
        
        public ObservableCollection<SubCategory> GetSubCategoriesList(int mainCategoryId = 0)
        {
            var items = Task.Run(async () => await repository.GetSubCategories(mainCategoryId));
            return new ObservableCollection<SubCategory>(items.Result);
        }

        public void OnMainCategoryChosen(object sender, EventArgs args)
        {
            if (SelectedMainCategory != null)
            {
                var mainCategoryId = SelectedMainCategory.MainCategoryId;
                SubCategoriesList = GetSubCategoriesList(mainCategoryId);
            }
            
        }
    }
}

