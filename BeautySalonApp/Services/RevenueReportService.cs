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
        private readonly CurrentBranchContext _CurrentBranchContext;

        public RevenueReportService()
        {
            _CurrentBranchContext = Program.ServiceProvider.GetRequiredService<CurrentBranchContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _globalContext = _databaseService.GetGlobalDbContext();
            _localContext = _databaseService.GetLocalDbContext(_CurrentBranchContext.BranchId);
        }

        public List<GlobalReport> GetReportsWithDetails(int BranchId)
        {
            return _globalContext.GlobalReports
                .Where(rr => rr.BranchId == _CurrentBranchContext.BranchId)
                .ToList();
        }

        public void GenerateAndSaveRevenueReport(DateTime startDate, DateTime endDate, int BranchId)
        {
            var appointments = _localContext.Appointments
                .Where(a => a.Date >= startDate.Date && a.Date <= endDate.Date)
                .Include(a => a.Service)
                .ToList();

            if (appointments.Count == 0)
            {
                MessageBox.Show("Нет данных для отчета за выбранный период.");
                return;
            }

            decimal totalRevenue = appointments.Sum(a => a.Service.Price);

            int numberOfClients = appointments.Select(a => a.CustomerId).Distinct().Count();

            var globalReport = new GlobalReport
            {
                BranchId = _CurrentBranchContext.BranchId,
                ReportDate = DateTime.Now,
                StartDate = startDate,
                EndDate = endDate,
                TotalIncome = totalRevenue,
                ClientsServed = numberOfClients
            };

            _globalContext.GlobalReports.Add(globalReport);
            _globalContext.SaveChanges();

            MessageBox.Show("Отчет успешно создан и сохранен.");
        }
    }
}
