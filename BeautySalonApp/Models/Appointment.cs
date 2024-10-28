using BeautySalonApp.Models.BeautySalonApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models

{
    public static class AppointmentStatusEnum
    {
        public const string Created = "created";
        public const string Success = "success";
        public const string Cancelled = "cancelled";
    }
    public static class AppointmentStatusDictionary
    {
        public static readonly Dictionary<string, string> Statuses = new Dictionary<string, string>
    {
        { AppointmentStatusEnum.Created, "Создан" },
        { AppointmentStatusEnum.Success, "Выполнен" },
        { AppointmentStatusEnum.Cancelled, "Отменен" }
    };
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
        public DateTime StartTime { get; set; }

        [Required]
        [Column("end_time")]
        public DateTime EndTime { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; }

        // Virtual Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
