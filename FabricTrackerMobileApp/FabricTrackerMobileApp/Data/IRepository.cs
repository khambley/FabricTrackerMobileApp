
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
        Task<List<Fabric>> GetFabrics();
        Task AddFabric(Fabric fabric);
        Task UpdateFabric(Fabric fabric);
        Task AddOrUpdate(Fabric fabric);
        Task DeleteFabric(Fabric fabric);
    }
}
