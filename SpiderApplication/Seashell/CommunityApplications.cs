using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Logging;
using Serilog;
using SpiderApplication.Seashell.PageHandlers;
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

        public CommunityApplications()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/Net6Tester.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            context = new SeashellContext();
            administrativeDistrictRepository = new AdministrativeDistrictRepository(context);
        }
        
        //The url should be the first page of xiaoqu list like the url in the default value of the parameter url
        public async Task<List<Community>> ReadCommunities(string url = SeashellConst.CommunityMainPageChanganURL)
        {
            string firstPage = string.Format(url, 0);

            int pageNum = await CommunityListPageHandler.ReadCommunityListPageNumber(firstPage);

            List<Community> communities = new List<Community>();

            for (int page = 1; page <= pageNum; page++)
            {
                List<Community> list = await CommunityListPageHandler.ReadCommunityListData(string.Format(url, page));

                communities = communities.Concat(list).ToList();
            }

            communities = ReadCommunityDetailInfo(communities);
            //communities.AsParallel().ForAll(community =>
            //{
            //    Community communityDetail = SeashellPageHandlers.ReadCommunityDetailData(community.SeashellURL).Result;

            //    community.BuildingNumber = communityDetail.BuildingNumber;
            //    community.Unit = communityDetail.Unit;
            //});

            return communities;
        }

        public List<Community> ReadCommunityDetailInfo(List<Community> communities)
        {
            Parallel.ForEach(communities, async community =>
            {
                Community communityDetail = new Community();
                try
                {
                    communityDetail = await CommunityPageHandler.ReadCommunityDetailData(community.SeashellURL);
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e, community.CommunityName + community.CommunityId);

                    community.BuildingNumber = 0;
                    community.Unit = 0;
                    return;
                }

                community.BuildingNumber = communityDetail.BuildingNumber;
                community.Unit = communityDetail.Unit;
            });

            return communities;
        }

        public int AddOrUpdateCommunities(IList<Community> communities)
        {
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

        public IList<AdministrativeDistrict> GetAdministrativeDistricts(Expression<Func<AdministrativeDistrict, bool>>? predicate = null)
        {
            if (predicate == null)
                return this.context.AdministrativeDistrict.ToList();
            else
                return this.context.AdministrativeDistrict.Where(predicate).ToList();           
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

            communities = ReadCommunityDetailInfo(communities);

            int updatedCount = AddOrUpdateCommunities(communities);

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

            communities = ReadCommunityDetailInfo(communities);

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
