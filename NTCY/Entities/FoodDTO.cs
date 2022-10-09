using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NTCY.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("Food")]
    public class FoodDTO
    {
        [Key]
        public int FoodId { get; set; }
        public string? FoodCode { get; set; }
        public string? FoodName { get; set; }
        public string? Category { get; set; }
        public string? FoodDescription { get; set; }
        public string? Quantity { get; set; }
        public double? Price { get; set; }
        public double? GST { get; set; }
        public string? Status { get; set; }
    }
}
