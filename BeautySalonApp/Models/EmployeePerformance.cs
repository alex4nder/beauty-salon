using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("employee_performance")]
    public class EmployeePerformance
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [Required]
        [Column("salon_id")]
        public int SalonId { get; set; }

        [Required]
        [Column("evaluation_date")]
        public DateTime EvaluationDate { get; set; }

        [Required]
        [Column("total_appointments")]
        public int TotalAppointments { get; set; }

        [Required]
        [Column("total_revenue")]
        public decimal TotalRevenue { get; set; }
    }
}
