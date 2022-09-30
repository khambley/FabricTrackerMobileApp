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
            FabricItem.MainCategoryId = SelectedMainCategory.MainCategoryId;
            FabricItem.SubCategoryId = SelectedSubCategory.SubCategoryId;
            await repository.AddOrUpdate(FabricItem);
            await Navigation.PopAsync();
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

            //var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            // get the image stream
            var stream = await photo.OpenReadAsync();

            //save the file as a Base64 string to model so we can save it in db
            using (var memoryStream = new System.IO.MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                var result = memoryStream.ToArray();
                FabricItem.ImageBase64 = Convert.ToBase64String(result);
            }

            //to display image
            ImageBytes = System.Convert.FromBase64String(FabricItem.ImageBase64);
            ImageSource = ImageSource.FromStream(() => new MemoryStream(ImageBytes));

            //ImagePath = newFile;
            return stream;
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

