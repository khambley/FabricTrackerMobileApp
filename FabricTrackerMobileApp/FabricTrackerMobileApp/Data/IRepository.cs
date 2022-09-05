
using FabricTrackerMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FabricTrackerMobileApp.Data
{
    public interface IRepository
    {
        event EventHandler<Fabric> OnItemAdded;
        event EventHandler<Fabric> OnItemUpdated;
        event EventHandler<Fabric> OnItemDeleted;

        event EventHandler<MainCategory> OnMainCategoryItemAdded;
        event EventHandler<MainCategory> OnMainCategoryItemUpdated;
        event EventHandler<MainCategory> OnMainCategoryItemDeleted;

        event EventHandler<SubCategory> OnSubCategoryItemAdded;
        event EventHandler<SubCategory> OnSubCategoryItemUpdated;
        event EventHandler<SubCategory> OnSubCategoryItemDeleted;

        Task<List<Fabric>> GetFabrics();
        Task AddFabric(Fabric fabric);
        Task UpdateFabric(Fabric fabric);
        Task AddOrUpdate(Fabric fabric);
        Task DeleteFabric(Fabric fabric);

        Task<List<MainCategory>> GetMainCategories();
        Task AddMainCategory(MainCategory mainCategory);
        Task UpdateMainCategory(MainCategory mainCategory);
        Task AddOrUpdateMainCategory(MainCategory mainCategory);
        Task DeleteMainCategory(MainCategory mainCategory);

        Task<List<SubCategory>> GetSubCategories(int Id);
    }
}
