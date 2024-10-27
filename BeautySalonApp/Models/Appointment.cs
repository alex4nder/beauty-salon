using BeautySalonApp.Models.BeautySalonApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    public enum AppointmentStatus
    {
        Created,
        Success,
        Cancelled
    }

    [Table("appointments")]
    public class Appointment
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Service")]
        [Column("service_id")]
        public Guid ServiceId { get; set; }

        [Required]
        [ForeignKey("Employee")]
        [Column("employee_id")]
        public Guid EmployeeId { get; set; }

        [Required]
        [ForeignKey("Customer")]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("description")]
        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("start_time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column("end_time")]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Column("status")]
        public AppointmentStatus Status { get; set; }

        // Virtual Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
