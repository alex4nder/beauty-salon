using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{

    [Table("managers")]
    public class Manager
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Branch")]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("first_name")]
        [MaxLength(36)]
        public string? FirstName { get; set; }

        [Column("last_name")]
        [MaxLength(36)]
        public string? LastName { get; set; }

        [Column("phone")]
        [MaxLength(19)]
        public string? Phone { get; set; }

        [Column("email")]
        [MaxLength(36)]
        public string? Email { get; set; }

        // Navigation Property
        public virtual Branch Branch { get; set; }
    }
}
