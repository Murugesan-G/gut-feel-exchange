using NTCYApplication.Models.Liquor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTCYApplication.Interfaces
{
    public interface ILiquorOrderArea
    {
        List<LiquorOrderArea> ViewAllLiquorOrders();
        string EditLiquorOrder(Dictionary<string, object> EditLiquorDictionary);
        List<LiquorOrderArea> ViewCompletedOrders();

    }
}