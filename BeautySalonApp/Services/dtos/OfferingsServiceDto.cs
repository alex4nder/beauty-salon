namespace BeautySalonApp.Services.dtos
{
    public class OfferingsServiceDto
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public bool IsPopular { get; set; }
    }
}
