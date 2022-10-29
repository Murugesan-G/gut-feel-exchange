using NTCY.Models.StockDetails;

namespace NTCY.Services.StockDetails
{
    public interface IStockService
    {
        public int AddStockInwardDetails(StockInward stockInward);
        public void AddStockInwardSubDetails(Dictionary<string, object> stockInwardSub, int iGrnNo);
        public void AddStockAdjustmentDetails(Dictionary<string, object> stockAdjustment);
        public string GetGrnNo();
    }
}
