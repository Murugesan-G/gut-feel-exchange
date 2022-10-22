namespace NTCY.Models.LiquorDetails
{
    public class LiquorOrderList
    {
        public int SubOrderId { get; set; }
        public int LiquorId { get; set; }
        public int OrderId { get; set; }
        public string? LiquorName { get; set; }
        public double? Quantity { get; set; }
        public string? Type { get; set; }
        public double? Price { get; set; }
        public string? Status { get; set; }
        public double? GST { get; set; }
        public string? ServedStatus { get; set; }
    }
}
