using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("managers")]
    public class Manager
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("login")]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public required string Login { get; set; }

        [Required]
        [Column("salon_id")]
        public int SalonId { get; set; }

        [Required]
        [Column("first_name")]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Column("email")]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string? Email { get; set; }

        [Required]
        [Column("phone")]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public required string Phone { get; set; }
    }
}
