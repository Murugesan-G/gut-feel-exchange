namespace NTCY.Models
{
    public class ClubCommitteeDet
    {
        public int CommitteeId { get; set; }
        public int ClubId { get; set; }
        public string? ClubName { get; set; }
        public List<ClubDetails>? clubDetails { get; set; }
        public string? Chairman { get; set; }
        public string? President { get; set; }
        public string? VicePresident { get; set; }
        public string? Secretary { get; set; }
        public string? Treasurer { get; set; }
        public string? CommitteeMembers { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class ClubDetails
    {
        public int ClubId { get; set; }
        public string? ClubName { get; set; }
    }
}
