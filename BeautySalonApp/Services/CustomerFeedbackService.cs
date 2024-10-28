using BeautySalonApp.Data;
using BeautySalonApp.Services.dtos;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class CustomerFeedbackService
    {
        private DatabaseService _databaseService;
        private readonly GlobalDbContext _globalContext;
        private LocalDbContext _localContext;
        private readonly CurrentBranchContext _CurrentBranchContext;

        public CustomerFeedbackService()
        {
            _CurrentBranchContext = Program.ServiceProvider.GetRequiredService<CurrentBranchContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _globalContext = _databaseService.GetGlobalDbContext();
            _localContext = _databaseService.GetLocalDbContext(_CurrentBranchContext.BranchId);
        }

        public List<CustomerReviewDto> GetCustomerFeedbacks()
        {
            var feedbacks = _globalContext.CustomerReviews
                .Where(rr => rr.BranchId == _CurrentBranchContext.BranchId)
                .ToList();

            var clients = _localContext.Customers.ToList();

            var clientFeedbacks = feedbacks.Select(cf => new CustomerReviewDto
            {
                CustomerFirstName = clients.FirstOrDefault(c => c.Id == cf.CustomerId)?.FirstName,
                CustomerLastName = clients.FirstOrDefault(c => c.Id == cf.CustomerId)?.LastName,
                CustomerEmail = clients.FirstOrDefault(c => c.Id == cf.CustomerId)?.Email,
                Comments = cf.Comment,
                Rating = cf.Rate,
                FeedbackDate = (DateTime)(cf.UpdatedAt == null ? cf.UpdatedAt : cf.CreatedAt),
            }).ToList();

            return clientFeedbacks;
        }
    }
}
