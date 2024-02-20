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

        [MaxLength(100)]
        public string External_id { get; set; }

        public decimal PlotRatio { get; set; }

        public DateTime UpdateDate { get; set; }

        public List<CommunityHistoryInfo> CommunityHistoryInfo { get; set; } = new List<CommunityHistoryInfo>();

        public List<Home> Home { get; set; } = new List<Home>();

        public void AddCommunityHistoryInfo(CommunityHistoryInfo entityToAdd)
        {
            if (CommunityHistoryInfo.Where(c => c.CommunityId == entityToAdd.CommunityId && c.DataTime.Date == entityToAdd.DataTime.Date).FirstOrDefault() == null)
                CommunityHistoryInfo.Add(entityToAdd);
        }
    }
}
