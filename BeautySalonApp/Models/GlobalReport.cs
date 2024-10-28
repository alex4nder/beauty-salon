using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("global_reports")]
    public class GlobalReport
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Branch")]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [Required]
        [Column("report_date")]
        public DateTime ReportDate { get; set; }

        [Column("clients_served")]
        public int? ClientsServed { get; set; }

        [Column("total_income")]
        public decimal? TotalIncome { get; set; }

        [Column("data")]
        public string? Data { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        // Navigation Property
        public virtual Branch Branch { get; set; }
    }

    public enum ReportType
    {
        IncomeReport,
        CustomerReport,
        ServiceReport
    }
}
