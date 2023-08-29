using NTCYApplication.Models;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface IClubCommmittee
    {
        List<ClubCommittee> ViewCommiteeMembers(); 

        //string AddMember(string Designation, string MembershipName, DateTime StartDate, DateTime EndDate, string Status);

        //string EditDesignation(string Designation, string MembershipName, DateTime StartDate, DateTime EndDate, string Status);

        string UpdateCommittee(Dictionary<string, object> ClubComitteeDictionary);

        //string AddTenureOfCommittee(DateTime StartDate, DateTime EndDate);

        //string EditTenureOfCommittee(DateTime StartDate, DateTime EndDate);
         
        string CreateCommittee(Dictionary<string, object> ClubComitteeDictionary);

        List<Member> ShowMembers();
        //string DeleteCommittee(int? CommitteeId); 
        Dictionary<string, object> SelectCommittee(int? CommitteeId);

        string CheckCommitteeTenure();

    } 
}
