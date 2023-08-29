using NTCYApplication.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface PaymentInterface
    {
        string InsertPaymentDetails(Dictionary<string, object> PaymentDictionary);
        string InsertIndividualPaymentDetails(Dictionary<string, object> PaymentDictionary);
        int DeleteBill(string membershipNo, DateTime billDate,string billType);

    }
}
