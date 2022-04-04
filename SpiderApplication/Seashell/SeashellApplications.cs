using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yang.Entities;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class SeashellApplications
    {
        //The url should be the first page of xiaoqu list like the url in the default value of the parameter url
        public async Task<List<Community>> ReadAllCommunities(string url = SeashellConst.CommunityMainPageXianURL)
        {
            string firstPage = string.Format(SeashellConst.CommunityMainPageXianURL, 1);

            int pageNum = await SeashellPageHandlers.ReadCommunityListPageNumber(firstPage);

            for (int page = 0; page < pageNum; page++)
            {
                SeashellPageHandlers.ReadCommunityListData(string.Format(SeashellConst.CommunityMainPageXianURL, page));
            }

            return new List<Community>();
        }
    }
}
