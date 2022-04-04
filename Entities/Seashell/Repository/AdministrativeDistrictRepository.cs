namespace Yang.Entities
{
    public static class AdministrativeDistrictRepository
    {
        public static AdministrativeDistrict GetByName(string communityName)
        {
            SeashellContext context = new SeashellContext();

            return context.AdministrativeDistrict.Where(a => a.AdministrativeDistrictName.Contains(communityName)).FirstOrDefault();
        }
    }
}
