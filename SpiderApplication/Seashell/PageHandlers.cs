using AngleSharp.Dom;
using Yang.Entities;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class PageHandlers
    {
        //The url will be like 'https://xa.ke.com/xiaoqu/'
        public static async Task<Community> ReadCommunityListData(string url)
        {
            IDocument document = await WebPageReader.GetPageAsync(url);

            return new Community();
        }

        //The url will be like 'https://xa.ke.com/xiaoqu/3820028098488153/'
        public static async Task<Community> ReadCommunityDetailData(string url) 
        {
            IDocument document = await WebPageReader.GetPageAsync(url);

            return new Community();
        }
    }
}
