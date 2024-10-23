using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("clients")]
    public class Client
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("first_name")]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [Column("phone")]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public required string Phone { get; set; }

        [Column("email")]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string? Email { get; set; }

        [Required]
        [Column("date_of_birth")]
        public required DateTime DateOfBirth { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
