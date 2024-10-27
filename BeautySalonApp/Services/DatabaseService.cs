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
            _configuration = configuration;
            _localDb1 = _configuration.GetConnectionString("BeautySalonLocal1");
            _localDb2 = _configuration.GetConnectionString("BeautySalonLocal2");
            _localDb3 = _configuration.GetConnectionString("BeautySalonLocal3");
            _globalDb = _configuration.GetConnectionString("BeautySalonGlobal");
        }

        public LocalDbContext GetLocalDbContext(int dbIndex)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<LocalDbContext>();

            var connectionString = dbIndex switch
            {
                1 => _configuration.GetConnectionString("BeautySalonLocal1"),
                2 => _configuration.GetConnectionString("BeautySalonLocal2"),
                3 => _configuration.GetConnectionString("BeautySalonLocal3"),
                _ => throw new ArgumentException("Invalid salonId")
            };

            //switch (dbIndex)
            //{
            //    case 1:
            //        optionsBuilder.UseMySql(_localDb1, ServerVersion.AutoDetect(_localDb1));
            //        break;
            //    case 2:
            //        optionsBuilder.UseMySql(_localDb2, ServerVersion.AutoDetect(_localDb2));
            //        break;
            //    case 3:
            //        optionsBuilder.UseMySql(_localDb3, ServerVersion.AutoDetect(_localDb3));
            //        break;
            //    default:
            //        throw new ArgumentException("Invalid database index");
            //}

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
