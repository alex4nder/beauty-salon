using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("revenue_reports")]
    public class RevenueReport
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("salon_id")]
        public int SalonId { get; set; }

        [Required]
        [Column("report_date")]
        public DateTime ReportDate { get; set; }

        [Required]
        [Column("report_period_start_date")]
        public DateTime ReportPeriodStartDate { get; set; }

        [Required]
        [Column("report_period_end_date")]
        public DateTime ReportPeriodEndDate { get; set; }

        [Required]
        [Column("total_revenue")]
        public decimal TotalRevenue { get; set; }

        [Required]
        [Column("number_of_clients")]
        public int NumberOfClients { get; set; }

        [Required]
        [Column("most_popular_service_id")]
        public int MostPopularServiceId { get; set; }
    }
}
