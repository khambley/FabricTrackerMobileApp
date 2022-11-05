using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public int CurrentTotalInches => FabricItem.TotalInches > 0 ? FabricItem.TotalInches : 0;

        public decimal? CurrentTotalYards => FabricItem.TotalYards != null ? FabricItem.TotalYards : 0;

        public decimal TotalYards { get; set; }

        public ObservableCollection<MainCategory> MainCategoriesList { get; set; }

        public ObservableCollection<SubCategory> SubCategoriesList { get; set; }

        public List<string> FabricTypesList { get; set; }

        public List<string> MaterialTypesList { get; set; }

        public bool ShowCategoryLabel
        {
            get
            {
                return !string.IsNullOrEmpty(FabricItem.MainCategoryName);
            }
        }

        public bool ShowItemCodeLabel
        {
            get
            {
                return !string.IsNullOrEmpty(FabricItem.ItemCode);
            }
        }

        public bool ShowNullItemCodeLabel
        {
            get
            {
                return string.IsNullOrEmpty(FabricItem.ItemCode);
            }
        }

        public FabricItemViewModel(Repository repository)
        {
            repository.OnMainCategoryItemAdded += (sender, item) => MainCategoriesList.Add(item);
            repository.OnMainCategoryItemAdded += (sender, item) => MainCategoriesList = GetMainCategoriesList();
            repository.OnMainCategoryItemAdded += (sender, item) => SelectedMainCategory = item;

            repository.OnSubCategoryItemAdded += (sender, item) => SubCategoriesList.Add(item);

            this.repository = repository;
            MainCategoriesList = GetMainCategoriesList();
            FabricTypesList = repository.GetFabricTypes();
            MaterialTypesList = repository.GetMaterialTypes();
            FabricItem = new Fabric();   
        }

        public MainCategory SelectedMainCategory { get; set; }

        public SubCategory SelectedSubCategory { get; set; }

        public int AddQty { get; set; }

        #region Commands

        public ICommand Save => new Command(async () =>
        {
            FabricItem.MainCategoryId = SelectedMainCategory != null ? SelectedMainCategory.MainCategoryId : FabricItem.MainCategoryId;
            FabricItem.SubCategoryId = SelectedSubCategory != null ? SelectedSubCategory.SubCategoryId : FabricItem.SubCategoryId;

            // Uncategorized - default maincategory / subcategory if none is chosen 
            if (FabricItem.MainCategoryId == 0)
            {
                FabricItem.MainCategoryId = 1;
            }
            if(FabricItem.SubCategoryId == 0)
            {
                FabricItem.SubCategoryId = 1;
            }

            FabricItem.TotalYards = (decimal)FabricItem.TotalInches / (decimal)36;

            await repository.AddOrUpdate(FabricItem);
            await Navigation.PopAsync();
        });

        public ICommand CalculateTotalYardsCommand => new Command(() =>
        {
            var totalInches = FabricItem.TotalInches + AddQty;
            if (totalInches < 0)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", "Qty total must be greater than or equal to 0", "OK");
                });
            }
            else
            {
                FabricItem.TotalInches = totalInches;
                TotalYards = FabricItem.TotalInches > 0 ? (decimal)FabricItem.TotalInches / (decimal)36.00 : 0;
            } 
        });

        public ICommand CaptureImageCommand => new Command(async () =>
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            var stream = await LoadPhotoAsync(photo);
        });

        
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
            if(SelectedMainCategory != null)
            {
                var subCategoryItemView = Resolver.Resolve<SubCategoryItemView>("mainCategoryItem", SelectedMainCategory);
                await Navigation.PushAsync(subCategoryItemView);
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", "You must select a Main Category first in order to add a Sub Category", "Go Back");
                });
            }
            
        });

        private async Task<Stream> LoadPhotoAsync(FileResult photo)
        {
            // cancelled
            if (photo == null)
                return null;

            if (FabricItem.ItemCode == null)
            {
                FabricItem.ItemCode = repository.GetUniqueItemCode();
            }

            var uniqueFileName = FabricItem.ItemCode + Path.GetExtension(photo.FileName);

            // get the image stream
            var stream = await photo.OpenReadAsync();

            // get the documents path on device
            var documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "images");

            // create images folder if it doesn't exist
            if (!Directory.Exists(documentsPath))
                Directory.CreateDirectory(documentsPath);

            // create image filename based on item code
            var newFile = Path.Combine(documentsPath, uniqueFileName);

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
            }
            else
            {
                return null;
            }
        }

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

        //public void OnSubCategoryChosen(object sender, EventArgs args)
        //{
        //    if (SelectedSubCategory != null)
        //    {
        //        var subCategoryId = SelectedSubCategory.SubCategoryId;
        //        SubCategoriesList = GetSubCategoriesList(subCategoryId);
        //    }
        //}
    }
}

