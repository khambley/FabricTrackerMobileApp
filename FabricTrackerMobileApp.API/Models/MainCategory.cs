using System.Collections.Generic;

namespace FabricTrackerMobileApp.API.Models
{
    public class MainCategory
    {
        public int MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }
        public List<SubCategory> SubCategories { get; set; }

    }
}
