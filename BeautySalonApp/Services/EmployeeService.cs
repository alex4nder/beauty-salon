using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class EmployeeService
    {
        private DatabaseService _databaseService;
        private readonly GlobalDbContext _globalContext;
        private LocalDbContext _localContext;
        private readonly CurrentSalonContext _currentSalonContext;

        public EmployeeService()
        {
            _currentSalonContext = Program.ServiceProvider.GetRequiredService<CurrentSalonContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _globalContext = _databaseService.GetGlobalDbContext();
            _localContext = _databaseService.GetLocalDbContext(_currentSalonContext.SalonId);
        }

        public void EmployeeAdd(Employee employee)
        {
            _localContext.Employees.Add(employee);
            _localContext.SaveChanges();
        }

        public List<Employee> GetEmployees()
        {
            return _localContext.Employees.ToList();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _localContext.Employees.Find(employeeId);
        }

        public void EmployeeRemove(int employeeId)
        {
            var employee = _localContext.Employees.Find(employeeId);
            if (employee != null)
            {
                _localContext.Employees.Remove(employee);
                _localContext.SaveChanges();
            }
        }

        public void EmployeeEdit(Employee employee)
        {
            var existingEmployee = _localContext.Employees.Find(employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Position = employee.Position;
                existingEmployee.WorkBookNumber = employee.WorkBookNumber;

                _localContext.SaveChanges();
            }
        }

        public List<Appointment>? GetAppointments(int employeeId)
        {
            var appointments = _localContext.Appointments
                .Where(a => a.EmployeeId == employeeId)
                .Include(a => a.Client)
                .Include(a => a.Service)
                .ToList();

            return appointments.Any() ? appointments : null;
        }

        public void UpdateAppointmentStatus(int appointmentId, string newStatus)
        {
            var appointment = _localContext.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                appointment.Status = newStatus;
                _localContext.SaveChanges();
            }
        }

        public Appointment GetAppointmentById(int id)
        {
            return _localContext.Appointments.Find(id);
        }

        public void AddAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null");
            }

            if (appointment.EndTime < appointment.StartTime)
            {
                throw new ArgumentException("The end date cannot be earlier than the start date.");
            }

            _localContext.Appointments.Add(appointment);
            _localContext.SaveChanges();
        }
    }
}
