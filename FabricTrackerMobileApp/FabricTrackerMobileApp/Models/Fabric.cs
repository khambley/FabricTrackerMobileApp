 using System;
using SQLite;

namespace FabricTrackerMobileApp.Models
{
    public class Fabric
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //Auto-generated when added to db
        public string ItemCode { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Designer { get; set; }

        public string ImagePath { get; set; }

        public string ImageBase64 { get; set; }

        public int MainCategoryId { get; set; }

        public string MainCategoryName { get; set; }

        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public int TotalInches { get; set; }

        public decimal? TotalYards { get; set; }

        public int? FatQtrQty { get; set; }

        public string FabricType { get; set; }

        public string MaterialType { get; set; }

        public string Location { get; set; }

        public int? Width { get; set; }

        public string BackgroundColor { get; set; }

        public string AccentColor1 { get; set; }

        public string AccentColor2 { get; set; }

        public string AccentColor3 { get; set; }

        public string Notes { get; set; }

        public string SourceName { get; set; }

        public string SourceUrl { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        public bool IsDiscontinued { get; set; }



    }
}
