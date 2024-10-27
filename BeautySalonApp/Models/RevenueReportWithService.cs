// @Deprecated
public class RevenueReportWithService
{
    public DateTime ReportDate { get; set; }
    public DateTime ReportPeriodStartDate { get; set; }
    public DateTime ReportPeriodEndDate { get; set; }
    public decimal TotalRevenue { get; set; }
    public string MostPopularService { get; set; }
    public int TotalCustomers { get; set; }
}