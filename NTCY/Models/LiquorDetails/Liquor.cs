namespace NTCY.Models.LiquorDetails
{
    public class Liquor
    {
        public int LiquorId { get; set; }
        public int LiquorCatId { get; set; }
        public string? LiquorName { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public bool? SoldInPegs { get; set; }
        public double? QuantityInMLBottle { get; set; }
        public double? PegsPerBottle { get; set; }
        public double? SellingPriceBottle { get; set; }
        public double? SellingPricePeg { get; set; }
        public double? GST { get; set; }
        public bool? Status { get; set; }
    }
}
