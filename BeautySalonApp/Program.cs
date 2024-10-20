using BeautySalonApp.Data;
using BeautySalonApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeautySalonApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
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
                services.AddTransient<MainForm>();

                // Зарегистрируйте репозитории и сервисы
                //services.AddScoped<ClientRepository>();
                //services.AddScoped<SalonRepository>();
            })
            .Build();

            Application.Run(host.Services.GetRequiredService<MainForm>());
        }
    }
}
