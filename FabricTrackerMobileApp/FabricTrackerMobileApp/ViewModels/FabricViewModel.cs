using System;
using System.IO;
using System.Threading.Tasks;
using FabricTrackerMobileApp.Data;
using FabricTrackerMobileApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.ViewModels
{
    public class FabricViewModel : ViewModelBase
    {
        private readonly Repository repository;
        public Fabric Item { get; private set; }

        public ImageSource ImageSource { get; set; }

        public byte[] ImageBytes { get; set; }

        public event EventHandler ItemStatusChanged;

        public FabricViewModel(Fabric item, Repository repository)
        {
            this.repository = repository;
            Item = item;
            ImageSource = LoadPhoto();
            Item.MainCategoryName = GetMainCategoryName();
            Item.SubCategoryName = GetSubCategoryName();
        }

        private ImageSource LoadPhoto()
        {
            //retrieve the image from ImagePath in db.
            var filePath = Item.ImagePath;
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

        public string GetMainCategoryName() => repository.GetMainCategoryById(Item.MainCategoryId).MainCategoryName;

        public string GetSubCategoryName()
        {
            return repository.GetSubCategoryById(Item.SubCategoryId).SubCategoryName;
        }


    }
}

