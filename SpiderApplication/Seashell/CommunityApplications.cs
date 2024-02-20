using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using SpiderApplication.Seashell.PageHandlers;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Yang.Entities;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class CommunityApplications
    {
        private SeashellContext context;
        private AdministrativeDistrictRepository administrativeDistrictRepository;
        private CommunityRepository communityRepo;

        public CommunityApplications()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/Net6Tester.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            context = new SeashellContext();
            administrativeDistrictRepository = new AdministrativeDistrictRepository(context);
            communityRepo = new CommunityRepository(this.context);
        }
        
        //The url should be the first page of xiaoqu list like the url in the default value of the parameter url
        protected async Task<IEnumerable<Community>> ReadCommunitiesByDistrict(string url = SeashellConst.CommunityMainPageChanganURL)
        {
            string firstPage = string.Format(url, 0);

            int pageNum = await CommunityListPageHandler.ReadCommunityListPageNumber(firstPage);

            IEnumerable<Community> communities = new List<Community>();
            CommunityListPageHandler communityListPageHandler = new CommunityListPageHandler();

            for (int page = 1; page <= pageNum; page++)
            {
                List<Community> list = await communityListPageHandler.ReadCommunityListData(string.Format(url, page));

                communities = communities.Concat(list).ToList();
            }

            communities = await ReadCommunityDetailInfoParallel(communities);

            return communities;
        }

        //Only including data on the community list page. This will significantly reduce the time to retreive community data.
        //The info on the community page(unit, building number, plot ratio will not change normally)
        protected async Task<IEnumerable<Community>> ReadCommunitiesOnListByDistrict(string url = SeashellConst.CommunityMainPageChanganURL)
        {
            string firstPage = string.Format(url, 0);

            int pageNum = await CommunityListPageHandler.ReadCommunityListPageNumber(firstPage);

            List<Community> communities = new List<Community>();
            CommunityListPageHandler communityListPageHandler = new CommunityListPageHandler();

            for (int page = 1; page <= pageNum; page++)
            {
                List<Community> list = await communityListPageHandler.ReadCommunityListData(string.Format(url, page));

                communities = communities.Concat(list).ToList();
            }

            return communities;
        }

        protected async Task<IEnumerable<Community>> ReadCommunityDetailInfoParallel(IEnumerable<Community> communities)
        {
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 50
            };

            await Parallel.ForEachAsync(communities, options, async (community, ct) =>
            {
                Community communityDetail = new Community();
                try
                {
                    communityDetail = await CommunityPageHandler.ReadCommunityDetailData(community.SeashellURL);
                    community.BuildingNumber = communityDetail.BuildingNumber;
                    community.Unit = communityDetail.Unit;
                    community.PlotRatio = communityDetail.PlotRatio;
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, community.CommunityName + community.SeashellURL);
                }
            });

             return communities;
        }

        protected async Task<IEnumerable<Community>> ReadCommunityHistoryInfoParallel(IEnumerable<Community> communities)
        {
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 50
            };

            await Parallel.ForEachAsync(communities, options, async (community, ct) =>
            {
                CommunityHistoryInfo communityHistoryInfo = new CommunityHistoryInfo();
                try
                {
                    communityHistoryInfo = await HomeListPageHandler.ReadCommunityHistoryInfo(string.Format(SeashellConst.CommunityHomeListURL, 1, community.External_id));
                    community.AddCommunityHistoryInfo(communityHistoryInfo);
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, community.CommunityName + string.Format(SeashellConst.CommunityHomeListURL, 1, community.External_id));
                }
            });

            return communities;
        }

        public async Task<int> GetAndSaveCommunityInfo(bool isOnlyRefreshBasicInfo = true)
        {
            IList<AdministrativeDistrict> districts = administrativeDistrictRepository.GetAll();

            IEnumerable<Community> communities = new ConcurrentBag<Community>();

            await Parallel.ForEachAsync(districts, async (district, ct) =>
            {
                IEnumerable<Community> communitiesByDistrict = isOnlyRefreshBasicInfo ? await ReadCommunitiesOnListByDistrict(district.CommunityMainPageURL) : await ReadCommunitiesByDistrict(district.CommunityMainPageURL);

                communities = communities.Concat(communitiesByDistrict);
            });

            int updatedCount = 0;

            updatedCount = communityRepo.AddOrUpdate(communities);

            Log.Logger.Information("Updated count:" + updatedCount);

            return updatedCount;
        }

        //This method is for refreshing the communities basic information(not including listing price and units)
        //in the database and will not read new community from seashell website.
        public async Task RefreshCommunityInfoInDatabase()
        {
            IEnumerable<Community> communitiesInDB = communityRepo.GetAll();

            communitiesInDB = await ReadCommunityDetailInfoParallel(communitiesInDB);

            communityRepo.AddOrUpdate(communitiesInDB);
        }

        public async Task<int> GetHistoryInfoForCommunitiesInDatabse()
        {
            IEnumerable<Community> communities = communityRepo.GetAll();

            communities = await ReadCommunityHistoryInfoParallel(communities);

            int updatedCounts = communityRepo.SaveCommunityHistoryInfo(communities);

            return updatedCounts;
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
