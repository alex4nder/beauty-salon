using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("first_name")]
        [MaxLength(255)]
        public string? FirstName { get; set; }

        [Column("last_name")]
        [MaxLength(255)]
        public string? LastName { get; set; }

        [Column("phone")]
        [MaxLength(18)]
        public string? Phone { get; set; }

        [Column("email")]
        [MaxLength(36)]
        public string? Email { get; set; }

        [Column("position")]
        [MaxLength(100)]
        public string? Position { get; set; }

        // Navigation Property
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

}
