using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NTCY.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("Room")]
    public class RoomDTO
    {
        [Key]
        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? RoomNo { get; set; }
        public double? Charges { get; set; }
        public double? GST { get; set; }
        public string? Status { get; set; }
        public string? RoomAllocated { get; set; }
    }
}
