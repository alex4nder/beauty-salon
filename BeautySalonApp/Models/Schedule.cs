using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    public enum WeekdayEnum
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    [Table("schedule")]
    public class Schedule
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Employee")]
        [Column("employee_id")]
        public Guid EmployeeId { get; set; }

        [Required]
        [Column("week_day")]
        public WeekdayEnum WeekDay { get; set; }

        [Required]
        [Column("start_time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column("end_time")]
        public TimeSpan EndTime { get; set; }

        // Navigation Property
        public virtual Employee Employee { get; set; }
    }
}
