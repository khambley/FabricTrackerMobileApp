using FabricTrackerMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

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
            await _connection.DeleteAsync(fabric);
            OnItemDeleted?.Invoke(this, fabric);
        }
        #endregion

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
                    MainCategoryName = "General"
                });
            }

            if (await _connection.Table<SubCategory>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new SubCategory()
                {
                    SubCategoryName = "Misc",
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
                    FabricType = "Woven",
                    Width = 60,
                    BackgroundColor = "Blue",
                    AccentColor1 = "Red",
                    Source = "Joann Fabrics",
                    PurchaseDate = DateTime.Now,
                    ReleaseDate = Convert.ToDateTime("2011-01-01"),
                    DateAdded = DateTime.Now,
                });
            }
        }

        #region MainCategories
        public async Task<List<MainCategory>> GetMainCategories()
        {
            await CreateConnection();
            return await _connection.Table<MainCategory>().ToListAsync();
        }

        
        public async Task AddMainCategory(MainCategory mainCategory)
        {
            await CreateConnection();
            await _connection.InsertAsync(mainCategory);
            OnMainCategoryItemAdded?.Invoke(this, mainCategory);
        }

        public async Task UpdateMainCategory(MainCategory mainCategory)
        {
            await CreateConnection();
            await _connection.UpdateAsync(mainCategory);
            OnMainCategoryItemUpdated?.Invoke(this, mainCategory);
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
            await _connection.DeleteAsync(mainCategory);
            OnMainCategoryItemDeleted?.Invoke(this, mainCategory);
        }
        #endregion

        public async Task<List<SubCategory>> GetSubCategories(int Id)
        {
            await CreateConnection();
            return await _connection.Table<SubCategory>().Where(mc => mc.MainCategoryId == Id).ToListAsync();
        }
        public async Task AddSubCategory(SubCategory subCategory)
        {
            await CreateConnection();
            await _connection.InsertAsync(subCategory);
            OnSubCategoryItemAdded?.Invoke(this, subCategory);
        }
        public async Task UpdateSubCategory(SubCategory subCategory)
        {
            await CreateConnection();
            await _connection.UpdateAsync(subCategory);
            OnSubCategoryItemUpdated?.Invoke(this, subCategory);
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
            await _connection.DeleteAsync(subCategory);
            OnSubCategoryItemDeleted?.Invoke(this, subCategory);
        }
    }
}
