using BeautySalonApp.Data;
using BeautySalonApp.Models;

namespace BeautySalonApp.Services
{
    public class EmployeePerformanceService
    {
        private readonly GlobalDbContext _globalContext;
        private readonly LocalDbContext _localContext;

        public EmployeePerformanceService(GlobalDbContext globalContext, LocalDbContext localContext)
        {
            _globalContext = globalContext;
            _localContext = localContext;
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
