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
                communities = communities.Concat(SeashellPageHandlers.ReadCommunityListData(string.Format(SeashellConst.CommunityMainPageXianURL, page)).Result).ToList();
            }

            //Task[] tasks = new Task[Convert.ToInt32(Math.Floor((decimal)communities.Count / 200)) + 1];
            //List<Task> taskList = new List<Task>();

            //for (int startIndex = 1; startIndex < communities.Count; startIndex += 200)
            //{
            //    int endIndex = startIndex + 199;
            //    if (endIndex >= communities.Count)
            //    {
            //        endIndex = communities.Count - 1;
            //    }

            //    taskList.Add(Task.Run(() =>
            //    {
            //        for (int i = startIndex; i <= endIndex; i++)
            //        {
            //            Community communityDetail = SeashellPageHandlers.ReadCommunityDetailData(communities[i].SeashellURL).Result;

            //            communities[i].BuildingNumber = communityDetail.BuildingNumber;
            //            communities[i].Unit = communityDetail.Unit;
            //        }
            //    }));
            //}

            //Task.WaitAll(taskList.ToArray());

            foreach (Community community in communities)
            {
                Community communityDetail = await SeashellPageHandlers.ReadCommunityDetailData(community.SeashellURL);

                community.BuildingNumber = communityDetail.BuildingNumber;
                community.Unit = communityDetail.Unit;
            }

            return communities;
        }

        public async Task<int> RefreshAllCommunityInfo()
        {
            List<Community> communities = await ReadAllCommunities();

            SeashellContext context = new SeashellContext();
            
            CommunityRepository repo = new CommunityRepository(context);

            repo.AddOrUpdate(communities);

            return communities.Count();
        }
    }
}
