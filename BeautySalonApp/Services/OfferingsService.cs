using BeautySalonApp.Data;
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
    }
}
