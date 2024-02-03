using Microsoft.Extensions.Logging;
using Serilog;
using Yang.Entities;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class SeashellApplications
    {
        private SeashellContext context;

        public SeashellApplications()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/Net6Tester.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            context = new SeashellContext();
        }
        
        //The url should be the first page of xiaoqu list like the url in the default value of the parameter url
        public async Task<List<Community>> ReadCommunities(string url = SeashellConst.CommunityMainPageBeilinURL)
        {
            string firstPage = string.Format(url, 1);

            int pageNum = await SeashellPageHandlers.ReadCommunityListPageNumber(firstPage);

            List<Community> communities = new List<Community>();

            for (int page = 1; page <= pageNum; page++)
            {
                communities = communities.Concat(SeashellPageHandlers.ReadCommunityListData(string.Format(url, page)).Result).ToList();
            }

            //communities.AsParallel().ForAll(community =>
            //{
            //    Community communityDetail = SeashellPageHandlers.ReadCommunityDetailData(community.SeashellURL).Result;

            //    community.BuildingNumber = communityDetail.BuildingNumber;
            //    community.Unit = communityDetail.Unit;
            //});

            return communities;
        }

        public async Task<List<Community>> ReadCommunityDetailInfo(List<Community> communities)
        {
            foreach (Community community in communities)
            {
                Community communityDetail = new Community();
                try
                {
                    communityDetail = await SeashellPageHandlers.ReadCommunityDetailData(community.SeashellURL);
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, community.CommunityName + community.CommunityId);

                    community.BuildingNumber = 0;
                    community.Unit = 0;
                    continue;
                }

                community.BuildingNumber = communityDetail.BuildingNumber;
                community.Unit = communityDetail.Unit;
            }

            return communities;
        }

        public async Task<int> GetAndRefreshAllCommunityInfo()
        {
            List<AdministrativeDistrict> districts = this.context.AdministrativeDistrict.ToList();

            List<Community> communities = new List<Community>();
                
            foreach (AdministrativeDistrict district in districts)
            {               
                List<Community> communitiesByDistrict = await ReadCommunities(district.CommunityMainPageURL);

                communities = communities.Concat(communitiesByDistrict).ToList();
            }

            communities = await ReadCommunityDetailInfo(communities);

            CommunityRepository repo = new CommunityRepository(this.context);

            int updatedCount = 0;
            foreach (Community communityEntity in communities)
            {
                try
                {
                    repo.AddOrUpdate(communityEntity);
                    updatedCount++;
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, communityEntity.CommunityName);

                    continue;
                }
            }

            repo.Save();

            return updatedCount;
        }

        public async Task<int> RefreshExistingCommunityBasicInfo()
        {
            List<AdministrativeDistrict> districts = this.context.AdministrativeDistrict.ToList();

            List<Community> communities = new List<Community>();

            foreach (AdministrativeDistrict district in districts)
            {
                List<Community> communitiesByDistrict = await ReadCommunities(district.CommunityMainPageURL);

                communities = communities.Concat(communitiesByDistrict).ToList();
            }

            CommunityRepository repo = new CommunityRepository(this.context);

            foreach (Community communityEntity in communities)
            {
                try
                {
                    repo.Update(communityEntity);
                }               
                catch (Exception e)
                {
                    Log.Logger.Error(e, communityEntity.CommunityName);

                    continue;
                }
            }

            repo.Save();

            return communities.Count();
        }

        public async Task<int> GetHistoryInfoForAllCommunities()
        {
            List<Community> communities = this.context.Communities.ToList();

            communities = await ReadCommunityDetailInfo(communities);

            CommunityRepository repo = new CommunityRepository(context);

            repo.AddOrUpdate(communities);

            repo.Save();

            return communities.Count();
        }

        //public async Task<List<Home>> ReadHomesByCommunity(Community community)
        //{
        //    ArgumentNullException.ThrowIfNull(community);

        //    string firstPage = string.Format(url, 1);

        //    int pageNum = await SeashellPageHandlers.ReadCommunityListPageNumber(firstPage);

        //    Home home = await CommunityHomePageHandler.ReadCommunityHomeDetail(community.HomeListURL);

            
        //}
    }
}
