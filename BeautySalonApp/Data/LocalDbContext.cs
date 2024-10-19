using BeautySalonApp.Models;
using System.Data.Entity;

namespace BeautySalonApp.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(string connectionString) : base(connectionString) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }
    }
}
