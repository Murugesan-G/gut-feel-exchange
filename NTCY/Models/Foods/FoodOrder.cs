namespace NTCY.Models.Foods
{
    public class FoodOrder
    {
        public int OrderId { get; set; }
        public string? UserName { get; set; }
        public string? MembershipNo { get; set; }
        public double? TotalAmount { get; set; }
        public double? GrossAmount { get; set; }
        public int TableNo { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; }
        public string? ModeOfPayment { get; set; }
        public int SubPaymentId { get; set; }
        public double? TotalGST { get; set; }
        public int SubOrderId { get; set; }
        public string? MemberName { get; set; }
        public string? FoodName { get; set; }
        public int Quantity { get; set; }
    }
}
