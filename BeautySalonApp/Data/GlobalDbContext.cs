using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace BeautySalonApp.Data
{
    public class GlobalDbContext : DbContext
    {
        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options) { }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<GlobalReport> GlobalReports { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Дополнительная настройка сущностей, если нужно
        }
    }
}
