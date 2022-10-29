
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
        public int StockSubId { get; set; }
        public int GrnId { get; set; }
        public int LiquorId { get; set; }
        public float PurchaseOrderRate { get; set; }
        public int PurchaseOrderQty { get; set; }
        public float MRP { get; set; }
        public float TaxAmount { get; set; }
        public float TaxPercentage { get; set; }
        public float DiscountAmount { get; set; }
        public float DiscountPercentage { get; set; }
        public int RejectedQty { get; set; }
        public int AcceptedQty { get; set; }
        public string? RejectedRemarks { get; set; }
    }
}
