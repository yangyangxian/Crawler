namespace Yang.Entities
{
    public class HomeListingPrice
    {
        public int HomeListingPriceId { get; set; }

        public int HomeId { get; set; }

        public Home Community { get; set; }

        public decimal ListingPrice { get; set; }

        public DateTime ListingPriceDate { get; set; }
    }
}
