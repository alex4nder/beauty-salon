using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class EmployeePerformanceService
    {
        private DatabaseService _databaseService;
        private readonly GlobalDbContext _globalContext;
        private LocalDbContext _localContext;
        private readonly CurrentSalonContext _currentSalonContext;

        public EmployeePerformanceService()
        {
            _currentSalonContext = Program.ServiceProvider.GetRequiredService<CurrentSalonContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _globalContext = _databaseService.GetGlobalDbContext();
            _localContext = _databaseService.GetLocalDbContext(_currentSalonContext.SalonId);
        }

        public List<EmployeePerformance> GetEmployeePerformanceReports(int salonId, int employeeId)
        {
            return _globalContext.EmployeePerformances
                .Where(ep => ep.SalonId == salonId && ep.EmployeeId == employeeId)
                .ToList();
        }

        //TODO: This is a DRAFT method. It's supposed to be used as MySQL Procedure
        public void UpdateEmployeePerformance()
        {
            var currentDate = DateTime.Today;

            var performanceData = _localContext.Appointments
                .Where(a => a.AppointmentDate.Date == currentDate)
                .GroupBy(a => a.EmployeeId)
                .Select(g => new
                {
                    EmployeeId = g.Key,
                    TotalAppointments = g.Count(),
                    TotalRevenue = g.Sum(a => a.Service.Price)
                })
                .ToList();

            foreach (var data in performanceData)
            {
                var performance = _globalContext.EmployeePerformances
                    .FirstOrDefault(ep => ep.EmployeeId == data.EmployeeId && ep.EvaluationDate.Date == currentDate);

                if (performance == null)
                {
                    performance = new EmployeePerformance
                    {
                        EmployeeId = data.EmployeeId,
                        SalonId = 1, //FIXME: select by a specific id dynamically
                        EvaluationDate = currentDate,
                        TotalAppointments = data.TotalAppointments,
                        TotalRevenue = data.TotalRevenue
                    };
                    _globalContext.EmployeePerformances.Add(performance);
                }
                else
                {
                    performance.TotalAppointments += data.TotalAppointments;
                    performance.TotalRevenue += data.TotalRevenue;
                }
            }

            _globalContext.SaveChanges();
        }
    }
}
