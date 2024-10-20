using BeautySalonApp.Data;

namespace BeautySalonApp.Services
{
    public class RevenueReportService
    {
        private readonly GlobalDbContext _globalContext;
        private readonly LocalDbContext _localContext;

        public RevenueReportService(GlobalDbContext globalContext, LocalDbContext localContext)
        {
            _globalContext = globalContext;
            _localContext = localContext;
        }

        public List<RevenueReportWithService> GetReportsWithDetails(int salonId)
        {
            // Получаем отчёты из глобальной базы данных
            var reports = _globalContext.RevenueReports
                .Where(rr => rr.SalonId == salonId)
                .ToList();

            // Получаем информацию о всех услугах из локальной базы данных
            var services = _localContext.Services.ToList();

            // Соединяем данные на уровне кода (в памяти)
            var reportWithService = reports.Select(rr => new RevenueReportWithService
            {
                ReportDate = rr.ReportDate,
                TotalRevenue = rr.TotalRevenue,
                MostPopularService = services.FirstOrDefault(s => s.Id == rr.MostPopularServiceId)?.ServiceName,
                TotalCustomers = rr.NumberOfClients
            }).ToList();

            return reportWithService;
        }
    }
}
