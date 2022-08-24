using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FabricTrackerMobileApp.API.Models;

namespace FabricTrackerMobileApp.API.Data
{
    public class FabricTrackerDbContext : DbContext
    {
        public FabricTrackerDbContext (DbContextOptions<FabricTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<FabricTrackerMobileApp.API.Models.Fabrics> Fabrics { get; set; }

        public DbSet<FabricTrackerMobileApp.API.Models.MainCategory> MainCategories { get; set; }

        public DbSet<FabricTrackerMobileApp.API.Models.SubCategory> SubCategories { get; set; }
    }
}
