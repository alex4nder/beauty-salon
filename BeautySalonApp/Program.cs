using BeautySalonApp.Data;
using BeautySalonApp.Forms;
using BeautySalonApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeautySalonApp
{
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

                services.AddScoped<SalonService>();
                services.AddScoped<RevenueReportService>();
                services.AddScoped<ClientFeedbackService>();
                services.AddScoped<ClientService>();
                services.AddScoped<EmployeeService>();
                services.AddScoped<ManagerService>();
                services.AddScoped<OfferingsService>();

                services.AddTransient<MainForm>();
                services.AddTransient<SalonForm>();
                services.AddTransient<ClientForm>();
                services.AddTransient<EmployeeForm>();
                services.AddTransient<ManagerForm>();
                services.AddTransient<EmployeeDetailsForm>();
            })
            .Build();

            ServiceProvider = host.Services;

            Application.Run(host.Services.GetRequiredService<MainForm>());
        }
    }
}
