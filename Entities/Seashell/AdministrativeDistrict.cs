using System.ComponentModel.DataAnnotations;

namespace Yang.Entities
{
    public class AdministrativeDistrict
    {
        public int AdministrativeDistrictId { get; set; }

        [MaxLength(20)]
        public string AdministrativeDistrictName { get; set; }

        public List<Community> Community { get; set; }    

        [MaxLength(200)]
        public string CommunityMainPageURL { get; set; }
    }
}
