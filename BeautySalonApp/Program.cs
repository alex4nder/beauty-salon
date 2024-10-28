using BeautySalonApp.Data;
using BeautySalonApp.Forms;
using BeautySalonApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeautySalonApp
{
    public class CurrentBranchContext
    {
        public int BranchId { get; set; }
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
                services.AddSingleton<CurrentBranchContext>();

                services.AddScoped<BranchService>();
                services.AddScoped<RevenueReportService>();
                services.AddScoped<CustomerFeedbackService>();
                services.AddScoped<CustomerService>();
                services.AddScoped<EmployeeService>();

                services.AddScoped<ManagerService>();
                services.AddScoped<OfferingsService>();

                services.AddTransient<MainForm>();
                services.AddTransient<SalonForm>();
                services.AddTransient<CustomerForm>();
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
