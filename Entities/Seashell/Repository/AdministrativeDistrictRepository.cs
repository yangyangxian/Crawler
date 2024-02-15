namespace Yang.Entities
{
    public class AdministrativeDistrictRepository : BaseRepository
    {
        public AdministrativeDistrictRepository(SeashellContext context) : base(context)
        {
            this.context = context;
        }
        
        public AdministrativeDistrict GetByName(string districtName)
        {
            return context.AdministrativeDistrict.Where(a => a.AdministrativeDistrictName.Contains(districtName)).FirstOrDefault();
        }

        public IList<AdministrativeDistrict> GetAll()
        {
            return context.AdministrativeDistrict.ToList();
        }
    }
}
