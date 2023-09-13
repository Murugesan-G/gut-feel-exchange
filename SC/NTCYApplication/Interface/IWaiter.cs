using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTCYApplication.Models.Club;

namespace NTCYApplication.Interfaces
{
    public interface IWaiter
    {
        DataSet GetWaiters(string Prefix);
    }
}
