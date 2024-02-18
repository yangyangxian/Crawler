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
        public async Task<IEnumerable<Community>> ReadCommunitiesByDistrict(string url = SeashellConst.CommunityMainPageChanganURL)
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

            communities = await ReadCommunityDetailInfo(communities);

            return communities;
        }

        //Only including data on the community list page. This will significantly reduce the time to retreive community data.
        //The info on the community page(unit, building number, plot ratio will not change normally)
        public async Task<IEnumerable<Community>> ReadCommunitiesBasicInfoByDistrict(string url = SeashellConst.CommunityMainPageChanganURL)
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

        public async Task<List<Community>> ReadCommunityDetailInfo(List<Community> communities)
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

                    community.BuildingNumber = 0;
                    community.Unit = 0;
                }
            });

             return communities;
        }

        public int AddOrUpdateCommunities(IEnumerable<Community> communities)
        {
            var groupByExternal =
                from community in communities
                group community by community.External_id;

            int updatedCount = 0;
            foreach (var communityGroup in groupByExternal)
            {
                try
                {
                    communityRepo.AddOrUpdate(communityGroup.First());
                    updatedCount++;
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, communityGroup.First().CommunityName);

                    continue;
                }
            }

            communityRepo.Save();

            return updatedCount;
        } 

        public IList<AdministrativeDistrict> GetAdministrativeDistricts(Expression<Func<AdministrativeDistrict, bool>>? predicate = null)
        {
            if (predicate == null)
                return this.context.AdministrativeDistrict.ToList();
            else
                return this.context.AdministrativeDistrict.Where(predicate).ToList();           
        }

        public async Task<int> RefreshAllCommunityInfo(bool isOnlyRefreshBasicInfo = true)
        {
            IList<AdministrativeDistrict> districts = administrativeDistrictRepository.GetAll();

            IEnumerable<Community> communities = new ConcurrentBag<Community>();

            await Parallel.ForEachAsync(districts, async (district, ct) =>
            {
                IEnumerable<Community> communitiesByDistrict = isOnlyRefreshBasicInfo ? await ReadCommunitiesBasicInfoByDistrict(district.CommunityMainPageURL) : await ReadCommunitiesByDistrict(district.CommunityMainPageURL);

                communities = communities.Concat(communitiesByDistrict);
            });

            int updatedCount = 0;

            updatedCount = AddOrUpdateCommunities(communities);

            Log.Logger.Information("Updated count:" + updatedCount);

            return updatedCount;
        }

        public async Task<int> RefreshCommunityBasicInfo()
        {
            IList<AdministrativeDistrict> districts = administrativeDistrictRepository.GetAll();

            IEnumerable<Community> communities = new ConcurrentBag<Community>();

            await Parallel.ForEachAsync(districts, async (district, ct) =>
            {
                IEnumerable<Community> communitiesByDistrict = await ReadCommunitiesBasicInfoByDistrict(district.CommunityMainPageURL);

                communities = communities.Concat(communitiesByDistrict);
            });

            int updatedCount = 0;

            updatedCount = AddOrUpdateCommunities(communities);

            Log.Logger.Information("Updated count:" + updatedCount);

            return updatedCount;
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
