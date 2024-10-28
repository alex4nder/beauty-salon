using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class EmployeeService
    {
        private DatabaseService _databaseService;
        private LocalDbContext _localContext;
        private readonly CurrentBranchContext _CurrentBranchContext;

        public EmployeeService()
        {
            _CurrentBranchContext = Program.ServiceProvider.GetRequiredService<CurrentBranchContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _localContext = _databaseService.GetLocalDbContext(_CurrentBranchContext.BranchId);
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

        public Employee GetEmployeeById(Guid employeeId)
        {
            return _localContext.Employees.Find(employeeId);
        }

        public void EmployeeRemove(Guid employeeId)
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

                _localContext.SaveChanges();
            }
        }

        public List<Appointment>? GetAppointments(Guid employeeId)
        {
            var appointments = _localContext.Appointments
                .Where(a => a.EmployeeId == employeeId)
                .Include(a => a.Customer)
                .Include(a => a.Service)
                .ToList();

            return appointments.Any() ? appointments : null;
        }

        public void UpdateAppointmentStatus(Guid appointmentId, AppointmentStatus newStatus)
        {
            var appointment = _localContext.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                appointment.Status = newStatus;
                _localContext.SaveChanges();
            }
        }

        public Appointment GetAppointmentById(Guid id)
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
