using NTCYApplication.Models.FrontOffice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NTCYApplication.Interfaces
{
    interface FrontOfficePaymentInterface
    {
        
        List<FrontOfficePayment> ViewAllFrontOfficePayment();
       
        List<FrontOfficePayment> ViewBillHistory();
        List<FrontOfficePayment> SearchBillHistory(Dictionary<string, object> FrontOfficePaymentIDictionary);
        Dictionary<string, List<FrontOfficePayment>> ViewDetailedBill(string MembershipNo,DateTime BillDate);
        Dictionary<string, List<FrontOfficePayment>> ViewDetailedBillHistory(string MembershipNo, DateTime BillDate,int Flag);
        List<FrontOfficePayment> Attendance();
        string DeleteServicesCollection(int ServCollectionId);
    }
}
