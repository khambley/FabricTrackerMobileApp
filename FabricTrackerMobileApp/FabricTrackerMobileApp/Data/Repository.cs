using FabricTrackerMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Data
{
    public class Repository : IRepository
    {
        private SQLiteAsyncConnection _connection;

        public event EventHandler<Fabric> OnItemAdded;
        public event EventHandler<Fabric> OnItemUpdated; 
        public event EventHandler<Fabric> OnItemDeleted;

        public event EventHandler<MainCategory> OnMainCategoryItemAdded;
        public event EventHandler<MainCategory> OnMainCategoryItemUpdated;
        public event EventHandler<MainCategory> OnMainCategoryItemDeleted;

        public event EventHandler<SubCategory> OnSubCategoryItemAdded;
        public event EventHandler<SubCategory> OnSubCategoryItemUpdated;
        public event EventHandler<SubCategory> OnSubCategoryItemDeleted;

        #region Fabrics
        public async Task<List<Fabric>> GetFabrics()
        {
            await CreateConnection();
            return await _connection.Table<Fabric>().ToListAsync();
        }

        public string GetUniqueItemCode()
        {
            var fabricsList = Task.Run(async () => await GetFabrics()).Result;

            var lastFabricInDB = fabricsList[fabricsList.Count - 1].Id;

            var newFabricId = lastFabricInDB + 1;

            string leadingZeroes = "";

            if (newFabricId > 0 && newFabricId < 10)
            {
                leadingZeroes = "0000";
            }
            else if (newFabricId > 9 && newFabricId < 100)
            {
                leadingZeroes = "000";
            }
            else if (newFabricId > 99 && newFabricId < 1000)
            {
                leadingZeroes = "00";
            }
            else if (newFabricId > 999 && newFabricId < 10000)
            {
                leadingZeroes = "0";
            }
            else
            {
                leadingZeroes = "";
            }
            var uniqueFabricItemCode = "FAB"
                + "-"
                + leadingZeroes
                + newFabricId.ToString()
                + "-"
                + Guid.NewGuid().ToString().ToUpper().Substring(0, 4);

            return uniqueFabricItemCode;

        }

        public async Task AddFabric(Fabric fabric)
        {
            await CreateConnection();
            await _connection.InsertAsync(fabric);
            OnItemAdded?.Invoke(this, fabric);
        }

        public async Task UpdateFabric(Fabric fabric)
        {
            await CreateConnection();
            await _connection.UpdateAsync(fabric);
            OnItemUpdated?.Invoke(this, fabric);
        }

        public async Task AddOrUpdate(Fabric fabric)
        {
            if(fabric.Id == 0)
            {
                await AddFabric(fabric);
            }
            else
            {
                await UpdateFabric(fabric);
            }
        }

        public async Task DeleteFabric(Fabric fabric)
        {
            await CreateConnection();
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Confirm the Deletion", $"Are you sure you want to delete the item, {fabric.Name}, from the database?", "OK", "Cancel");
                if (answer)
                {
                    await _connection.DeleteAsync(fabric);
                    OnItemUpdated?.Invoke(this, fabric);
                }
            });
            //await _connection.DeleteAsync(fabric);
            //OnItemDeleted?.Invoke(this, fabric);
        }
        public List<string> GetFabricTypes()
        {
            return new List<string>()
            {
                "",
                "Woven",
                "Chambray",
                "Cordurory",
                "Crepe",
                "Denim",
                "Flannel",
                "Fleece",
                "Jersey",
                "Knit",
                "Outdoor",
                "Suiting",
                "Terry Cloth"
            };
        }
        public List<string> GetMaterialTypes()
        {
            return new List<string>()
            {
                "",
                "Cotton",
                "Chenille",
                "Denim",
                "Felt",
                "Jacquard",
                "Linen",
                "Polyester",
                "Rayon",
                "Satin",
                "Silk",
                "Velvet",
                "Wool",
            };
        }
        #endregion

        #region CreateConnection
        private async Task CreateConnection()
        {
            if (_connection != null)
            {
                return;
            }

            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var databasePath = Path.Combine(documentPath, "FabricTrackerMobileApp.db");

            _connection = new SQLiteAsyncConnection(databasePath);

            await _connection.CreateTableAsync<MainCategory>();
            await _connection.CreateTableAsync<SubCategory>();
            await _connection.CreateTableAsync<Fabric>();

            if (await _connection.Table<MainCategory>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new MainCategory()
                {
                    MainCategoryName = "Uncategorized"
                });
            }

            if (await _connection.Table<SubCategory>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new SubCategory()
                {
                    SubCategoryName = "Uncategorized",
                    MainCategoryId = 1
                });
            }

            if (await _connection.Table<Fabric>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new Fabric()
                {
                    ItemCode = "001",
                    Name = "General Test Fabric",
                    ImagePath = "general-test-fabric.jpg",
                    MainCategoryId = 1,
                    SubCategoryId = 1,
                    TotalInches = 36,
                    TotalYards = (decimal)1.00,
                    FabricType = "Woven",
                    Width = 60,
                    BackgroundColor = "Blue",
                    AccentColor1 = "Red",
                    SourceName = "Sample Source Name",
                    PurchaseDate = DateTime.Now,
                    ReleaseDate = Convert.ToDateTime("2011-01-01"),
                    DateAdded = DateTime.Now,
                });
            }
        }
        #endregion
        
        #region MainCategories

        public async Task<List<MainCategory>> GetMainCategories()
        {
            await CreateConnection();
            var list = await _connection.Table<MainCategory>().OrderBy(x => x.MainCategoryName).ToListAsync();
            return list;
        }

        public MainCategory GetMainCategoryById(int Id)
        {
            Task.Run(async () => await CreateConnection());
            var mainCategory = _connection.Table<MainCategory>().Where(mc => mc.MainCategoryId == Id).FirstOrDefaultAsync();
            return mainCategory.Result;
        }

        public async Task AddMainCategory(MainCategory mainCategory)
        {
            await CreateConnection();
            var mainCategoriesList = await GetMainCategories();

            //check if maincategory already exists
            bool exists = false;
            foreach (var item in mainCategoriesList)
            {
                if (item.MainCategoryName.ToLower() == mainCategory.MainCategoryName.ToLower())
                {
                    exists = true;
                    break;
                }               
            }
            if (!exists)
            {
                await _connection.InsertAsync(mainCategory);
                OnMainCategoryItemAdded?.Invoke(this, mainCategory);
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Saved", $"{mainCategory.MainCategoryName} was saved successfully in the database.", "OK");
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", $"{mainCategory.MainCategoryName} category already exists in database", "OK");
                });
            }

        }

        public async Task UpdateMainCategory(MainCategory mainCategory)
        {
            await CreateConnection();
            var mainCategoriesList = await GetMainCategories();

            //check if maincategory already exists
            bool exists = false;
            foreach (var item in mainCategoriesList)
            {
                if (item.MainCategoryName.ToLower() == mainCategory.MainCategoryName.ToLower())
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                await _connection.UpdateAsync(mainCategory);
                OnMainCategoryItemUpdated?.Invoke(this, mainCategory);
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", $"{mainCategory.MainCategoryName} already exists in database", "OK");
                });
            }
        }

        public async Task AddOrUpdateMainCategory(MainCategory mainCategory)
        {
            if (mainCategory.MainCategoryId == 0)
            {
                await AddMainCategory(mainCategory);
            }
            else
            {
                await UpdateMainCategory(mainCategory);
            }
        }

        public async Task DeleteMainCategory(MainCategory mainCategory)
        {
            await CreateConnection();

            var subCategories = await GetSubCategories(mainCategory.MainCategoryId);

            

            if (mainCategory.MainCategoryName.ToLower() == ("Uncategorized").ToLower())
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", $"{mainCategory.MainCategoryName} main category cannot be deleted", "OK");
                });  
            }
            // subcategories check
            else if (subCategories.Count > 0)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", $"{mainCategory.MainCategoryName} category contains subcategories. You must delete all subcategories first before you can delete the main category", "OK");
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    bool answer = await App.Current.MainPage.DisplayAlert("Confirm the Deletion", $"Are you sure you want to delete the category, {mainCategory.MainCategoryName}, from the database?", "OK", "Cancel");
                    if (answer)
                    {
                        await _connection.DeleteAsync(mainCategory);
                        OnMainCategoryItemUpdated?.Invoke(this, mainCategory);
                    }
                });
            }
        }
        #endregion

        #region SubCategories

        public async Task<List<SubCategory>> GetSubCategories(int Id)
        {
            await CreateConnection();
            return await _connection.Table<SubCategory>().Where(mc => mc.MainCategoryId == Id).OrderBy(sc => sc.SubCategoryName).ToListAsync();
        }

        public SubCategory GetSubCategoryById(int Id)
        {
            Task.Run(async () => await CreateConnection());
            var subCategory = _connection.Table<SubCategory>().Where(sc => sc.SubCategoryId == Id).FirstOrDefaultAsync();
            return subCategory.Result;

        }

        public async Task AddSubCategory(SubCategory subCategory)
        {
            await CreateConnection();
            var subCategoriesList = await GetSubCategories(subCategory.MainCategoryId);

            //check if subCategory already exists
            bool exists = false;
            foreach (var item in subCategoriesList)
            {
                if (item.SubCategoryName.ToLower() == subCategory.SubCategoryName.ToLower())
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                await _connection.InsertAsync(subCategory);
                OnSubCategoryItemAdded?.Invoke(this, subCategory);
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", $"{subCategory.SubCategoryName} category already exists in database", "OK");
                });
            }
        }
        public async Task UpdateSubCategory(SubCategory subCategory)
        {
            await CreateConnection();
            var subCategoriesList = await GetSubCategories(subCategory.MainCategoryId);

            //check if subCategory already exists
            bool exists = false;
            foreach (var item in subCategoriesList)
            {
                if (item.SubCategoryName.ToLower() == subCategory.SubCategoryName.ToLower())
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                await _connection.UpdateAsync(subCategory);
                OnSubCategoryItemUpdated?.Invoke(this, subCategory);
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", $"{subCategory.SubCategoryName} category already exists in database", "OK");
                });
            }
        }
        public async Task AddOrUpdateSubCategory(SubCategory subCategory)
        {
            if (subCategory.SubCategoryId == 0)
            {
                await AddSubCategory(subCategory);
            }
            else
            {
                await UpdateSubCategory(subCategory);
            }
        }
        public async Task DeleteSubCategory(SubCategory subCategory)
        {
            await CreateConnection();
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Confirm the Deletion", $"Are you sure you want to delete the subcategory, {subCategory.SubCategoryName}, from the database?", "OK", "Cancel");
                if (answer)
                {
                    await _connection.DeleteAsync(subCategory);
                    OnSubCategoryItemUpdated?.Invoke(this, subCategory);
                }
            });
        }
        #endregion
    }
}
