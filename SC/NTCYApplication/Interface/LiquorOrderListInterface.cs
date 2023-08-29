using NTCYApplication.Models.Liquor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace NTCYApplication.Interfaces
{
    public interface LiquorOrderListInterface
    {
        int CreateLiquorOrderList(Dictionary<string, object> FoodOrderListDictionary);
        List<LiquorOrderList> ViewAllLiquorOrderDetails();
    }
}
