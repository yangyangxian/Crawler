using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yang.Entities.Seashell
{
    public class CommunityHistoryInfo
    {
        public int CommunityId { get; set; }

        public DateTime DataTime { get; set; }

        public decimal CommunityListingPrice { get; set; }

        public int CommunityListingUnits { get; set; }
    }
}
