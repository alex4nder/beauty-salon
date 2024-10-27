using BeautySalonApp.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class ClientFeedbackService
    {
        private DatabaseService _databaseService;
        private readonly GlobalDbContext _globalContext;
        private LocalDbContext _localContext;
        private readonly CurrentSalonContext _currentSalonContext;

        public ClientFeedbackService()
        {
            _currentSalonContext = Program.ServiceProvider.GetRequiredService<CurrentSalonContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _globalContext = _databaseService.GetGlobalDbContext();
            _localContext = _databaseService.GetLocalDbContext(_currentSalonContext.SalonId);
        }

        public List<ClientFeedbackWithService> GetClientFeedbacks(int salonId)
        {
            var feedbacks = _globalContext.ClientFeedbacks
                .Where(rr => rr.SalonId == salonId)
                .ToList();

            var services = _localContext.Services.ToList();
            var clients = _localContext.Clients.ToList();

            var clientFeedbackWithService = feedbacks.Select(cf => new ClientFeedbackWithService
            {
                Service = services.FirstOrDefault(s => s.Id == cf.ServiceId)?.ServiceName,
                ClientFirstName = clients.FirstOrDefault(c => c.Id == cf.ClientId)?.FirstName,
                ClientLastName = clients.FirstOrDefault(c => c.Id == cf.ClientId)?.LastName,
                ClientEmail = clients.FirstOrDefault(c => c.Id == cf.ClientId)?.Email,
                Comments = cf.Comments,
                Rating = cf.Rating,
                FeedbackDate = cf.FeedbackDate,
            }).ToList();

            return clientFeedbackWithService;
        }
    }
}
