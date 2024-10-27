using BeautySalonApp.Data;
using BeautySalonApp.Models;
using BeautySalonApp.Services.dtos;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class OfferingsService
    {
        private DatabaseService _databaseService;
        private readonly GlobalDbContext _globalContext;
        private LocalDbContext _context;
        private readonly CurrentSalonContext _currentSalonContext;

        private const int PopularityThreshold = 5;

        public OfferingsService()
        {
            _currentSalonContext = Program.ServiceProvider.GetRequiredService<CurrentSalonContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _globalContext = _databaseService.GetGlobalDbContext();
            _context = _databaseService.GetLocalDbContext(_currentSalonContext.SalonId);
        }

        public void ServiceAdd(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public List<OfferingsServiceDto> GetServices()
        {
            var currentDate = DateTime.Now;
            var oneMonthAgo = currentDate.AddMonths(-1);

            var services = _context.Services
                .Select(service => new OfferingsServiceDto
                {
                    Id = service.Id,
                    ServiceName = service.ServiceName,
                    Description = service.Description,
                    Price = service.Price,
                    Duration = service.Duration,
                    IsPopular = service.Appointments.Count(a => a.AppointmentDate >= oneMonthAgo) >= PopularityThreshold,
                })
                .ToList();

            return services;
        }

        public Service GetServiceById(int serviceId)
        {
            return _context.Services.Find(serviceId);
        }

        public void ServiceRemove(int serviceId)
        {
            var service = _context.Services.Find(serviceId);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
        }

        public void ServiceEdit(Service service)
        {
            var existingService = _context.Services.Find(service.Id);
            if (existingService != null)
            {
                existingService.ServiceName = service.ServiceName;
                existingService.Description = service.Description;
                existingService.Price = service.Price;
                existingService.Duration = service.Duration;

                _context.SaveChanges();
            }
        }
    }
}
