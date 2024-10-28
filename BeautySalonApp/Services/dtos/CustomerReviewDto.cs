namespace BeautySalonApp.Services.dtos
{
    public class CustomerReviewDto
    {

        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}
