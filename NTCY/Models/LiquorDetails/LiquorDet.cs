namespace NTCY.Models
{
    public class LiquorDet
    {
        public int LiquorId { get; set; }
        public int LiquorCatId { get; set; }
        public List<LiquorCategoryDet>? Categories { get; set; }
        public string? LiquorName { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public bool? SoldInPegs { get; set; }
        public double? QuantityInMLBottle { get; set; }
        public double? PegsPerBottle { get; set; }
        public double? SellingPriceBottle { get; set; }
        public double? SellingPricePeg { get; set; }
        public double? GST { get; set; }
        public bool? Status { get; set; }
        public string? CategoryName { get; set; }
    }

    public class LiquorCategoryDet
    {
        public int LiquorCatId { get; set; }
        public string? CategoryName { get; set; }
    }

    //public class AvailableStatus
    //{
    //    public int Status { get; set; }
    //    public string? StatusName { get; set; }
    //}
}
