namespace Yang.Entities
{
    public class HomeListingPrice
    {
        public int HomeListingPriceId { get; set; }

        public int HomeId { get; set; }

        public Home Home { get; set; }

        public decimal ListingPrice { get; set; }

        public DateTime ListingPriceDate { get; set; }
    }
}
