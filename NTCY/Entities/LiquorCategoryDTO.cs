using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NTCY.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("LiquorCategory")]
    public class LiquorCategoryDTO
    {
        [Key]
        public int LiquorCatId { get; set; }
        public string? CategoryName { get; set; }
    }
}
