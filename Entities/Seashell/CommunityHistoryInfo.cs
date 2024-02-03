namespace Yang.Entities
{
    public class CommunityHistoryInfo
    {
        public int CommunityHistoryInfoId { get; set; }

        public int CommunityId { get; set; }

        public string CommunityName { get; set; }

        public Community Community { get; set; }

        public DateTime DataTime { get; set; }

        public decimal CommunityListingPrice { get; set; }

        public int CommunityListingUnits { get; set; }
    }
}
