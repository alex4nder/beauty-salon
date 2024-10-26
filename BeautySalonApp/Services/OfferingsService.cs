using BeautySalonApp.Data;
using BeautySalonApp.Models;
using BeautySalonApp.Services.dtos;

namespace BeautySalonApp.Services
{
    public class OfferingsService
    {
        private readonly LocalDbContext _context;
        private const int PopularityThreshold = 5;

        public OfferingsService(LocalDbContext context)
        {
            _context = context;
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

        public void RemoveService(int serviceId)
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
