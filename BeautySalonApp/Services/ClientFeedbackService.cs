using BeautySalonApp.Data;

namespace BeautySalonApp.Services
{
    public class ClientFeedbackService
    {
        private readonly GlobalDbContext _globalContext;
        private readonly LocalDbContext _localContext;

        public ClientFeedbackService(GlobalDbContext globalContext, LocalDbContext localContext)
        {
            _globalContext = globalContext;
            _localContext = localContext;
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
