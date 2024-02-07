using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yang.Entities
{
    public class Community
    {
        public int CommunityId { get; set; }

        [MaxLength(100)]
        public string CommunityName { get; set; }

        public int? AdministrativeDistrictId { get; set; }

        public AdministrativeDistrict AdministrativeDistrict { get; set; }

        [MaxLength(100)]
        public string Neighborhood { get; set; }

        public int Unit { get; set; }

        public int BuildingNumber { get; set; }

        [MaxLength(300)]
        public string SeashellURL { get; set; }

        [MaxLength(300)]
        public string HomeListURL { get; set; }

        [MaxLength(100)]
        public string External_id { get; set; }

        public DateTime UpdateDate { get; set; }

        public List<CommunityHistoryInfo> CommunityHistoryInfo { get; set; } = new List<CommunityHistoryInfo>();

        public List<Home> Home { get; set; } = new List<Home>();
    }
}
