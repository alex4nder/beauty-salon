using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("employees")]
    public class Employee
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

        [Required]
        [Column("position")]
        [MaxLength(50)]
        public required string Position { get; set; }

        [Required]
        [Column("work_book_number")]
        [MaxLength(50)]
        public required string WorkBookNumber { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
