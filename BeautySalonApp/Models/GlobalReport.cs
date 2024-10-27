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
        public Guid BranchId { get; set; }

        [Required]
        [Column("report_type")]
        public ReportType ReportType { get; set; }

        [Required]
        [Column("report_date")]
        public DateTime ReportDate { get; set; }

        [Column("clients_served")]
        public int? ClientsServed { get; set; }

        [Column("total_income")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? TotalIncome { get; set; }

        [Column("data")]
        public string? Data { get; set; }

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
