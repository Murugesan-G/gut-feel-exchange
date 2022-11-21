
namespace NTCY.Models.StockDetails
{
    public class StockInward
    {
        public int GrnId { get; set; }
        public string? GrnNo { get; set; }
        public DateTime? GrnDate { get; set; }
        public int PurchaseOrderNo { get; set; }
        public int PermitNo { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public int DeliveryChallanNo { get; set; }
        public DateTime? Deliverydate { get; set; }
        public string? Supplier { get; set; }
        public double? TotalAmount { get; set; }
        public double? TotalTax { get; set; }
        public double? TotalDiscount { get; set; }
        public double? NetAmount { get; set; }
        public int UserId { get; set; }
        public string? Remarks { get; set; }
    }

    public class StockInwardSub
    {
        public string? StockSubId { get; set; }
        public string? GrnId { get; set; }
        public string? LiquorId { get; set; }
        public string? LiquorName { get; set; }
        public string? PurchaseOrderRate { get; set; }
        public string? PurchaseOrderQty { get; set; }
        public string? MRP { get; set; }
        public string? TaxAmount { get; set; }
        public string? TaxPercentage { get; set; }
        public string? DiscountAmount { get; set; }
        public string? DiscountPercentage { get; set; }
        public string? RejectedQty { get; set; }
        public string? AcceptedQty { get; set; }
        public string? RejectedRemarks { get; set; }
    }

    public class StockInwardCommon
    {
        public StockInward stockInward { get; set; }
        public StockInwardSub stockInwardSub { get; set; }
    }
}
