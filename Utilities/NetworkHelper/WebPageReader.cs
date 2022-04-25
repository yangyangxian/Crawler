using AngleSharp;
using AngleSharp.Dom;

namespace Yang.Utilities
{
    public class WebPageReader
    {
        public static async Task<IDocument> GetPageAsync(string url)
        {
            ArgumentNullException.ThrowIfNull(url);

            if (!url.StartsWith("https:") && !url.StartsWith("http:"))
                throw new ArgumentException("url must start with http or https");

            var config = Configuration.Default.WithDefaultLoader();

            var document = await BrowsingContext.New(config).OpenAsync(url);

            return document;
        }
    }
}