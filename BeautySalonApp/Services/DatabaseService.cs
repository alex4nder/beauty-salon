using BeautySalonApp.Data;

namespace BeautySalonApp.Services
{
    public class DatabaseService
    {
        private readonly string _localDb1 = System.Configuration.ConfigurationManager.ConnectionStrings["LocalDb1"].ConnectionString;
        private readonly string _localDb2 = System.Configuration.ConfigurationManager.ConnectionStrings["LocalDb2"].ConnectionString;
        private readonly string _localDb3 = System.Configuration.ConfigurationManager.ConnectionStrings["LocalDb3"].ConnectionString;
        private readonly string _globalDb = System.Configuration.ConfigurationManager.ConnectionStrings["GlobalDb"].ConnectionString;

        private LocalDbContext GetLocalDbContext(int dbIndex)
        {
            switch (dbIndex)
            {
                case 1:
                    return new LocalDbContext(_localDb1);
                case 2:
                    return new LocalDbContext(_localDb2);
                case 3:
                    return new LocalDbContext(_localDb3);
                default:
                    throw new ArgumentException("Invalid database index");
            }
        }

        private GlobalDbContext GetGlobalDbContext(int dbIndex)
        {
            return new GlobalDbContext(_globalDb);
        }
    }
}
