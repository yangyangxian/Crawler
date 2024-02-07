using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yang.Entities
{
    public class Home
    {
        public int HomeId { get; set; }

        public int CommunityId { get; set; }

        public Community Community { get; set; }

        public int BuildingTotalFloors { get; set; }
        
        public decimal ConstructionArea { get; set; }

        public decimal FloorArea { get; set; }

        [MaxLength(1000)]
        public string FloorAreaDetail { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public List<HomeListingPrice> HomeListingPrice { get; set; } = new List<HomeListingPrice>();

        [MaxLength(100)]
        public string External_id { get; set; }

        [MaxLength(300)]
        public string SeashellURL { get; set; }
    }
}
