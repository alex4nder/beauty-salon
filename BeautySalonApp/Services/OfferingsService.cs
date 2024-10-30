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
        private readonly CurrentBranchContext _CurrentBranchContext;

        private const int PopularityThreshold = 2;

        public OfferingsService()
        {
            _CurrentBranchContext = Program.ServiceProvider.GetRequiredService<CurrentBranchContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _globalContext = _databaseService.GetGlobalDbContext();
            _context = _databaseService.GetLocalDbContext(_CurrentBranchContext.BranchId);
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
                    ServiceName = service.Title,
                    Description = service.Description,
                    Price = service.Price,
                    Duration = (int)(service.Duration == null ? 0 : service.Duration),
                    IsPopular = service.Appointments.Count(a => a.Date >= oneMonthAgo) >= PopularityThreshold,
                })
                .ToList();

            return services;
        }

        public Service GetServiceById(Guid serviceId)
        {
            return _context.Services.Find(serviceId);
        }

        public void ServiceRemove(Guid serviceId)
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
                existingService.Title = service.Title;
                existingService.Description = service.Description;
                existingService.Price = service.Price;
                existingService.Duration = service.Duration;

                _context.SaveChanges();
            }
        }
    }
}
