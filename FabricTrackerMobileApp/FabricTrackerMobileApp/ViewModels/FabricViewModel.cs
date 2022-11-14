﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public decimal TotalYards { get; set; }

        public event EventHandler ItemStatusChanged;

        public FabricViewModel(Fabric item, Repository repository)
        {
            this.repository = repository;
            Item = item;
            ImageSource = LoadPhoto();
            Item.MainCategoryName = GetMainCategoryName();
            Item.SubCategoryName = GetSubCategoryName();
            TotalYards = Item.TotalInches != null ? (decimal)Item.TotalInches / 36 : 0;

        }
        public ICommand DeleteFabricCommand => new Command(async () =>
        {
            await repository.DeleteFabric(Item);
        });

        public ImageSource LoadPhoto()
        {
            //retrieve the image from ImagePath in db.

            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Data/images/" + Item.ImagePath);

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

