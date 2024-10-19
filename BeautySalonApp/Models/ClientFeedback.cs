using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("client_feedbacks")]
    public class ClientFeedback
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("client_id")]
        public int ClientId { get; set; }

        [Required]
        [Column("salon_id")]
        public int SalonId { get; set; }

        [Required]
        [Column("service_id")]
        public int ServiceId { get; set; }

        [Required]
        [Column("feedback_date")]
        public DateTime FeedbackDate { get; set; }

        [Required]
        [Column("rating")]
        public decimal Rating { get; set; }

        [Column("comments")]
        public string? Comments { get; set; }
    }
}
