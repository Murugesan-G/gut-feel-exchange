namespace NTCY.Models.RoomDetails
{
    public class Room
    {
        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? RoomNo { get; set; }
        public double? Charges { get; set; }
        public double? GST { get; set; }
        public string? Status { get; set; }
        public string? RoomAllocated { get; set; }
    }
}
