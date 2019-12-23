using AngleSharp;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    class EastCrawler
    {
        public StringBuilder data = new StringBuilder();

        public async void GetData()
        {

            var config = Configuration.Default.WithDefaultLoader();
            // Load the names of all The Big Bang Theory episodes from Wikipedia
            var address = "https://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes";
            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(address);
            // This CSS selector gets the desired content
            var cellSelector = "tr.vevent td:nth-child(3)";
            // Perform the query to get all cells with the content
            var cells = document.QuerySelectorAll(cellSelector);
            // We are only interested in the text - select it with LINQ
            var titles = cells.Select(m => m.TextContent);

            foreach (var item in titles)
            {
                data.Append(item);
            }
        }
    }
}
