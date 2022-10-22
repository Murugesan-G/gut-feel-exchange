namespace NTCY.Models.Foods
{
    public class FoodOrderList
    {
        public int SubOrderId { get; set; }
        public int FoodId { get; set; }
        public int OrderId { get; set; }
        public string? FoodName { get; set; }
        public double? Quantity { get; set; }
        public string? Type { get; set; }
        public double? Price { get; set; }
        public string? Status { get; set; }
        public double? GST { get; set; }
        public string? ServedStatus { get; set; }
    }
}
