using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.Models
{
    public abstract class BaseEntity 
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
