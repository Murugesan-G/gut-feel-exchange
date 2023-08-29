using NTCYApplication.Models;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface IMember
    {
        int Save();

        string CreateMember(Dictionary<string, string> MemberDictionary);

        string UpdateMember(Dictionary<string, string> MemberDictionary);

        Dictionary<string, object> ViewMemberDetails(int MemId);
        List<Member> ViewAllMembers();
        string DeleteMember(int MemId);
        List<Subscription> ShowMemberShipTypes();
        string GenerateSubscriptionBill(Dictionary<string, object> MemberDictionary);
        Dictionary<string, object> SelectMember(int? MemId);
        DataSet SearchMembers(string Prefix);
        DataSet GetMembers(string Prefix);
        DataSet GetMember(string sMembershipNo);
        string GetMemberMobileNo(string mNo);
        DataSet GetMemberDue(string mNo);
        int SaveSMSLog(string MNo,string MobNo, float Amount,string Status,string Batch_Id);

    }
}
