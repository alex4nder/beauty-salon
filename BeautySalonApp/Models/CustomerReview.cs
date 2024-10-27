using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("customer_review")]
    public class CustomerReview
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Customer")]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [ForeignKey("Branch")]
        [Column("branch_id")]
        public Guid BranchId { get; set; }

        [Required]
        [Column("rate")]
        public int Rate { get; set; }

        [Column("comment")]
        [MaxLength(255)]
        public string? Comment { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public virtual Branch Branch { get; set; }
    }
}
