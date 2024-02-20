using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Net;

namespace Yang.Utilities
{
    public class WebPageReader
    {
        public static async Task<IDocument> GetPageAsync(string url)
        {
            ArgumentNullException.ThrowIfNull(url);

            if (!url.StartsWith("https:") && !url.StartsWith("http:"))
                throw new ArgumentException("url must start with http or https");

            var requester = new DefaultHttpRequester("Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");

            var config = Configuration.Default.WithDefaultLoader().With(requester);

            IBrowsingContext context = BrowsingContext.New(config);

            var document = await context.OpenAsync(url);

            return document;
        }

        public static string GetDynamicPage(string url, SeashellConst.By targetElementby = 0, string byValue = null)
        {
            ArgumentNullException.ThrowIfNull(url);
            if (targetElementby > 0)
                ArgumentNullException.ThrowIfNull(byValue);

            string dynamicText = string.Empty;

            using (IWebDriver driver = new ChromeDriver())
            {
                // Navigate to the page with dynamic content
                driver.Navigate().GoToUrl(url);

                // Optionally, wait for a certain condition to be true
                if (targetElementby != 0)
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    switch (targetElementby) {
                        case SeashellConst.By.TagName:
                            wait.Until(d => d.FindElement(By.TagName(byValue)));
                            break;
                        case SeashellConst.By.ClassName:
                            wait.Until(d => d.FindElement(By.ClassName(byValue)));
                            break;
                        case SeashellConst.By.Id:
                            wait.Until(d => d.FindElement(By.Id(byValue)));
                            break;
                    }

                    // Now that the dynamic content has loaded, you can interact with it
                    // For example, get the text of an element with id 'dynamic-content'
                    IWebElement dynamicElement = null;
                    switch (targetElementby)
                    {
                        case SeashellConst.By.TagName:
                            dynamicElement = driver.FindElement(By.TagName(byValue));
                            break;
                        case SeashellConst.By.ClassName:
                            dynamicElement = driver.FindElement(By.ClassName(byValue));
                            break;
                        case SeashellConst.By.Id:
                            dynamicElement = driver.FindElement(By.Id(byValue));
                            break;
                    }
                    dynamicText = dynamicElement.Text;
                }
                else
                {
                    dynamicText = driver.FindElement(By.TagName("body")).Text;
                }

                // Don't forget to close the browser
                driver.Quit();
            }

            return dynamicText;
        }
    }
}