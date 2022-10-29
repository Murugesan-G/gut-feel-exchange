namespace NTCY.Models.StockDetails
{
    public class StockAdjustment
    {
        public int StockId { get; set; }
        public int LiquorId { get; set; }
        public string? LiquorName { get; set; }
        public int UserId { get; set; }
        public string? GrnNo { get; set; }
        public DateTime Date { get; set; }
        public double CurrentStockBottles { get; set; }
        public double CurrentStockPegs { get; set; }
        public double Qty_Bottles { get; set; }
        public double Qty_Pegs { get; set; }
        public string? Add_Sub { get; set; }
        public double PegAmount { get; set; }
        public double BottleAmount { get; set; }
        public string? Remarks { get; set; }
    }
}
