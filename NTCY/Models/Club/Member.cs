namespace NTCY.Models.Club
{
    public class Member
    {
        public string? MembershipNo { get; set; }        
        public string? MemberName { get; set; }
        public string? MobileNo { get; set; }
        public string? WatsupNo { get; set; }
        public string? EmailId { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Qualification { get; set; }
        public string? Occupation { get; set; }
        public string? PrimaryProximatyCard { get; set; }
        public string? AadharAddress { get; set; }
        public string? CommunictionAddress { get; set; }
        public string? AreasOfInterest { get; set; }
        public string? SpouseName { get; set; }
        public string? Child1sName { get; set; }
        public string? Child2sName { get; set; }
        public string? Child3sName { get; set; }
        public DateTime? DOBSpouse { get; set; }
        public DateTime? DateOfMarriage { get; set; }
        public DateTime? DOBChild1 { get; set; }
        public DateTime? DOBChild2 { get; set; }
        public DateTime? DOBChild3 { get; set; }
        public bool? MemberAlive { get; set; }
        public bool? MemberShipStatus { get; set; }
        public decimal? MemberShipAmount { get; set; }
        public string? MembershipCategory { get; set; }
        public DateTime? DateOfInduction { get; set; }
        public DateTime? DateOfExpiry { get; set; }
        public string? Nationality { get; set; }
        public string? SpouseId { get; set; }
        public string? ChildId1 { get; set; }
        public string? ChildId2 { get; set; }
        public string? ChildId3 { get; set; }
        public bool? PhotoExist { get; set; }
        public string? MemberPhotoPath { get; set; }
        public string? SpousePhotoPath { get; set; }
        public string? Child1PhotoPath { get; set; }
        public string? Child2PhotoPath { get; set; }
        public string? Child3PhotoPath { get; set; }
    }
}
