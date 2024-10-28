using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class GlobalReportService
    {
        private DatabaseService _databaseService;
        private readonly GlobalDbContext _globalContext;
        private readonly LocalDbContext _localContext;
        private readonly CurrentBranchContext _CurrentBranchContext;

        public GlobalReportService()
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

        public void UpdateGlobalReports(int BranchId, DateTime startDate, DateTime endDate)
        {
            _globalContext.Database.ExecuteSqlRaw("CALL UpdateGlobalReports({0}, {1}, {2})", BranchId, startDate, endDate);
        }
    }
}
