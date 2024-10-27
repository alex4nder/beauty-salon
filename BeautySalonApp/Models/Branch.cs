using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("branches")]
    public class Branch
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("location")]
        [MaxLength(255)]
        public string? Location { get; set; }

        [Column("title")]
        [MaxLength(255)]
        public string? Title { get; set; }

        [Column("phone")]
        [MaxLength(18)]
        public string? Phone { get; set; }

        [Column("contact_info")]
        [MaxLength(255)]
        public string? ContactInfo { get; set; }

        // Navigation Properties
        public virtual ICollection<Manager> Managers { get; set; } = new List<Manager>();
        public virtual ICollection<GlobalReport> GlobalReports { get; set; } = new List<GlobalReport>();
        public virtual ICollection<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();
    }
}
