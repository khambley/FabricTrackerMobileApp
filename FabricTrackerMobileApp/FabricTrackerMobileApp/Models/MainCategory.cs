using SQLite;
using System.Collections.Generic;

namespace FabricTrackerMobileApp.Models
{
    public class MainCategory
    {
        [PrimaryKey, AutoIncrement]
        public int MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }
        //public List<SubCategory> SubCategories { get; set; }

    }
}
