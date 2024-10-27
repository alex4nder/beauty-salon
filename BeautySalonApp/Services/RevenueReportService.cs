using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class RevenueReportService
    {
        private DatabaseService _databaseService;
        private readonly GlobalDbContext _globalContext;
        private readonly LocalDbContext _localContext;
        private readonly CurrentSalonContext _currentSalonContext;

        public RevenueReportService()
        {
            _currentSalonContext = Program.ServiceProvider.GetRequiredService<CurrentSalonContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _globalContext = _databaseService.GetGlobalDbContext();
            _localContext = _databaseService.GetLocalDbContext(_currentSalonContext.SalonId);
        }

        public List<RevenueReportWithService> GetReportsWithDetails(int salonId)
        {
            var reports = _globalContext.RevenueReports
                .Where(rr => rr.SalonId == salonId)
                .ToList();

            var services = _localContext.Services.ToList();

            var reportWithService = reports.Select(rr => new RevenueReportWithService
            {
                ReportDate = rr.ReportDate,
                ReportPeriodStartDate = rr.ReportPeriodStartDate,
                ReportPeriodEndDate = rr.ReportPeriodEndDate,
                TotalRevenue = rr.TotalRevenue,
                MostPopularService = services.FirstOrDefault(s => s.Id == rr.MostPopularServiceId)?.ServiceName,
                TotalCustomers = rr.NumberOfClients
            }).ToList();

            return reportWithService;
        }

        public void GenerateAndSaveRevenueReport(DateTime startDate, DateTime endDate, int salonId)
        {
            var appointments = _localContext.Appointments
                .Where(a => a.AppointmentDate >= startDate.Date && a.AppointmentDate <= endDate.Date)
                .Include(a => a.Service)
                .ToList();

            if (appointments.Count == 0)
            {
                MessageBox.Show("Нет данных для отчета за выбранный период.");
                return;
            }

            decimal totalRevenue = appointments.Sum(a => a.Service.Price);

            int numberOfClients = appointments.Select(a => a.ClientId).Distinct().Count();

            int mostPopularServiceId = appointments
                .GroupBy(a => a.ServiceId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            var revenueReport = new RevenueReport
            {
                SalonId = salonId,
                ReportDate = DateTime.Now,
                ReportPeriodStartDate = startDate,
                ReportPeriodEndDate = endDate,
                TotalRevenue = totalRevenue,
                NumberOfClients = numberOfClients,
                MostPopularServiceId = mostPopularServiceId
            };

            _globalContext.RevenueReports.Add(revenueReport);
            _globalContext.SaveChanges();

            MessageBox.Show("Отчет успешно создан и сохранен.");
        }
    }
}
