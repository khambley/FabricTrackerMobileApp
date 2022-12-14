using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabricTrackerMobileApp.API.Models
{
    public class Fabrics
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Designer { get; set; }
        public string ImagePath { get; set; }
        public int MainCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int TotalInches { get; set; }
        public int FatQtrQty { get; set; }
        public string FabricType { get; set; }
        public int Width { get; set; }
        public string BackgroundColor { get; set; }
        public string AccentColor1 { get; set; }
        public string AccentColor2 { get; set; }
        public string AccentColor3 { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDiscontinued { get; set; }



    }
}
