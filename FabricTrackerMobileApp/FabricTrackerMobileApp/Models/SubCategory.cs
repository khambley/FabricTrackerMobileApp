using SQLite;

namespace FabricTrackerMobileApp.Models
{
    public class SubCategory
    {
        [PrimaryKey, AutoIncrement]
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int MainCategoryId { get; set; }
    }
}
