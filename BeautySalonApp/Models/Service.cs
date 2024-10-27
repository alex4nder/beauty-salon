using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("services")]
    public class Service
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(36)]
        public string Title { get; set; }

        [Column("description")]
        [MaxLength(255)]
        public string? Description { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("duration")]
        public int? Duration { get; set; }

        // Navigation Property
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
