
using FabricTrackerMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FabricTrackerMobileApp.Data
{
    public interface IRepository
    {
        Task<List<Fabric>> GetFabrics();
        Task AddFabric(Fabric fabric);
        Task UpdateFabric(Fabric fabric);
        Task AddOrUpdate(Fabric fabric);
        Task DeleteFabric(Fabric fabric);
    }
}
