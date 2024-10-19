using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace BeautySalonApp.Data
{
    public class GlobalDbContext : DbContext
    {
        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options) { }

        public DbSet<Salon> Salons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<EmployeePerformance> EmployeePerformances { get; set; }
        public DbSet<RevenueReport> RevenueReports { get; set; }
        public DbSet<ClientFeedback> ClientFeedbacks { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Дополнительная настройка сущностей, если нужно
        }
    }
}
