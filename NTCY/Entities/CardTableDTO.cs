using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NTCY.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("CardTable")]

    public class CardTableDTO
    {
        [Key]
        public int TableNo { get; set; }
        public string? TableName { get; set; }
        public string? Status { get; set; }
    }
}
