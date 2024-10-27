using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonApp.Services
{
    public class EmployeeService
    {
        private readonly GlobalDbContext _globalContext;
        private readonly LocalDbContext _localContext;

        public EmployeeService(GlobalDbContext globalContext, LocalDbContext localContext)
        {
            _globalContext = globalContext;
            _localContext = localContext;
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
    }
}
