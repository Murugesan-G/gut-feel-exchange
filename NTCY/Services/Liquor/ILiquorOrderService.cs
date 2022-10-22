using NTCY.Models.LiquorDetails;

namespace NTCY.Services.LiquorOrderS
{
    public interface ILiquorOrderService
    {
        public List<Models.LiquorDetails.Liquor> ViewAllLiquorDetails(string type);
        public List<LiquorOrder> ViewAllLiquorOrder();
        public List<LiquorOrder> ViewCompletedOrders();
    }
}
