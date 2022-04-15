namespace Yang.Entities
{
    public class AdministrativeDistrictRepository : BaseRepository
    {
        public AdministrativeDistrictRepository(SeashellContext context) : base(context)
        {
            this.context = context;
        }
        
        public AdministrativeDistrict GetByName(string communityName)
        {
            return context.AdministrativeDistrict.Where(a => a.AdministrativeDistrictName.Contains(communityName)).FirstOrDefault();
        }
    }
}
