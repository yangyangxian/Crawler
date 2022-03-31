using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yang.Entities.Seashell
{
    public class Community
    {
        public int CommunityId { get; set; }

        public string CommunityName { get; set; }

        public string AdministrativeDistrict { get; set; }

        public string Neighborhood { get; set; }

        public string Unit { get; set; }

        public string BuildingNumber { get; set; }

        public string External_fb_expo_id { get; set; }

        public CommunityHistoryInfo communityHistoryInfo { get; set; }
    }
}
