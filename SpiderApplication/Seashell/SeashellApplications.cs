using Yang.Entities;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class SeashellApplications
    {
        //The url should be the first page of xiaoqu list like the url in the default value of the parameter url
        public async Task<List<Community>> ReadAllCommunities(string url = SeashellConst.CommunityMainPageXianURL)
        {
            string firstPage = string.Format(url, 1);

            int pageNum = await SeashellPageHandlers.ReadCommunityListPageNumber(firstPage);

            List<Community> communities = new List<Community>();

            for (int page = 1; page <= pageNum; page++)
            {
                communities = communities.Concat(SeashellPageHandlers.ReadCommunityListData(string.Format(url, page)).Result).ToList();
            }

            foreach (Community community in communities)
            {
                Community communityDetail = await SeashellPageHandlers.ReadCommunityDetailData(community.SeashellURL);

                community.BuildingNumber = communityDetail.BuildingNumber;
                community.Unit = communityDetail.Unit;
            }

            //communities.AsParallel().ForAll(community =>
            //{
            //    Community communityDetail = SeashellPageHandlers.ReadCommunityDetailData(community.SeashellURL).Result;

            //    community.BuildingNumber = communityDetail.BuildingNumber;
            //    community.Unit = communityDetail.Unit;
            //});

            return communities;
        }

        public async Task<int> RefreshAllCommunityInfo()
        {
            SeashellContext context = new SeashellContext();

            List<AdministrativeDistrict> districts = context.AdministrativeDistrict.ToList();

            List<Community> communities = new List<Community>();
                
            foreach (AdministrativeDistrict district in districts)
            {               
                List<Community> communitiesByDistrict = await ReadAllCommunities(district.CommunityMainPageURL);

                communities = communities.Concat(communitiesByDistrict).ToList();
            }
         
            CommunityRepository repo = new CommunityRepository(context);

            repo.AddOrUpdate(communities);

            return communities.Count();
        }
    }
}
