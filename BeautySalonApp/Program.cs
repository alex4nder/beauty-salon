using BeautySalonApp.Data;
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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<LocalDbContext>(options =>
                    options.UseMySql(context.Configuration.GetConnectionString("BeautySalonLocal1")));
                services.AddDbContext<LocalDbContext>(options =>
                    options.UseMySql(context.Configuration.GetConnectionString("BeautySalonLocal2")));
                services.AddDbContext<LocalDbContext>(options =>
                    options.UseMySql(context.Configuration.GetConnectionString("BeautySalonLocal3")));
                services.AddDbContext<GlobalDbContext>(options =>
                    options.UseMySql(context.Configuration.GetConnectionString("BeautySalonGlobal")));

                // Зарегистрируйте репозитории и сервисы
                //services.AddScoped<ClientRepository>();
                //services.AddScoped<SalonRepository>();
                // Добавьте другие регистрации по мере необходимости
            })
            .Build();

            Application.Run(new MainForm());
        }
    }
}