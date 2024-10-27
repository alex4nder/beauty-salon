using BeautySalonApp.Data;
using BeautySalonApp.Forms;
using BeautySalonApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeautySalonApp
{
    public class CurrentSalonContext
    {
        public int SalonId { get; set; }
    }

    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<LocalDbContext>(options =>
                    options.UseMySql(context.Configuration.GetConnectionString("BeautySalonLocal1"),
                        ServerVersion.AutoDetect(context.Configuration.GetConnectionString("BeautySalonLocal1"))));

                services.AddDbContext<LocalDbContext>(options =>
                    options.UseMySql(context.Configuration.GetConnectionString("BeautySalonLocal2"),
                        ServerVersion.AutoDetect(context.Configuration.GetConnectionString("BeautySalonLocal2"))));

                services.AddDbContext<LocalDbContext>(options =>
                    options.UseMySql(context.Configuration.GetConnectionString("BeautySalonLocal3"),
                        ServerVersion.AutoDetect(context.Configuration.GetConnectionString("BeautySalonLocal3"))));

                services.AddDbContext<GlobalDbContext>(options =>
                    options.UseMySql(context.Configuration.GetConnectionString("BeautySalonGlobal"),
                        ServerVersion.AutoDetect(context.Configuration.GetConnectionString("BeautySalonGlobal"))));

                services.AddSingleton<DatabaseService>();
                services.AddSingleton<CurrentSalonContext>();

                services.AddScoped<SalonService>();
                services.AddScoped<RevenueReportService>();
                services.AddScoped<ClientFeedbackService>();
                services.AddScoped<ClientService>();
                services.AddScoped<EmployeeService>();
                //services.AddScoped<EmployeeService>(provider =>
                //{
                //    var currentSalonContext = provider.GetRequiredService<CurrentSalonContext>();
                //    var salonId = currentSalonContext.SalonId;
                //    var databaseService = provider.GetRequiredService<DatabaseService>();
                //    var globalContext = databaseService.GetGlobalDbContext();
                //    var localContext = databaseService.GetLocalDbContext(salonId);
                //    return new EmployeeService(globalContext, localContext);

                //    //return new EmployeeService(dbService.GetGlobalDbContext(), context);

                //});
                services.AddScoped<ManagerService>();
                services.AddScoped<OfferingsService>();

                services.AddTransient<MainForm>();
                services.AddTransient<SalonForm>();
                services.AddTransient<ClientForm>();
                services.AddTransient<EmployeeForm>();
                services.AddTransient<ManagerForm>();
                services.AddTransient<EmployeeDetailsForm>();
                services.AddTransient<AppointmentForm>();
            })
            .Build();

            ServiceProvider = host.Services;

            Application.Run(host.Services.GetRequiredService<MainForm>());
        }
    }
}
