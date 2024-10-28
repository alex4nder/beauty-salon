using BeautySalonApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BeautySalonApp.Services
{
    public class DatabaseService
    {
        private readonly string _localDb1;
        private readonly string _localDb2;
        private readonly string _localDb3;
        private readonly string _globalDb;

        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _localDb1 = GetConnectionString("BeautySalonLocal1");
            _localDb2 = GetConnectionString("BeautySalonLocal2");
            _localDb3 = GetConnectionString("BeautySalonLocal3");
            _globalDb = GetConnectionString("BeautySalonGlobal");
        }

        private string GetConnectionString(string name)
        {
            var connectionString = _configuration.GetConnectionString(name);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"Database connection string '{name}' is not found or empty.");
            }
            return connectionString;
        }
        public LocalDbContext GetLocalDbContext(int dbIndex)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<LocalDbContext>();

            var connectionString = dbIndex switch
            {
                1 => _configuration.GetConnectionString("BeautySalonLocal1"),
                2 => _configuration.GetConnectionString("BeautySalonLocal2"),
                3 => _configuration.GetConnectionString("BeautySalonLocal3"),
                _ => throw new ArgumentException("Invalid Value", nameof(dbIndex))
            };

            var optionsBuilder = new DbContextOptionsBuilder<LocalDbContext>()
              .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new LocalDbContext(optionsBuilder.Options);
        }

        public GlobalDbContext GetGlobalDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<GlobalDbContext>();
            optionsBuilder.UseMySql(_globalDb, ServerVersion.AutoDetect(_globalDb));

            return new GlobalDbContext(optionsBuilder.Options);
        }
    }
}
