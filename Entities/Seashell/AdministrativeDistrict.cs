namespace Yang.Entities
{
    public class AdministrativeDistrict
    {
        public int AdministrativeDistrictId { get; set; }

        public string AdministrativeDistrictName { get; set; }

        public List<Community> Community { get; set; }    
    }
}
