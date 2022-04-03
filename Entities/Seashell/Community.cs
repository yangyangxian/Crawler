using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yang.Entities
{
    public class Community
    {
        public int CommunityId { get; set; }

        public string CommunityName { get; set; }

        [NotMapped]
        public string AdministrativeDistrict { get; set; }

        [NotMapped]
        public string Neighborhood { get; set; }

        [NotMapped]
        public string Unit { get; set; }

        [NotMapped]
        public string BuildingNumber { get; set; }

        [NotMapped]
        public string External_fb_expo_id { get; set; }

        [NotMapped]
        public CommunityHistoryInfo CommunityHistoryInfo { get; set; }
    }
}
