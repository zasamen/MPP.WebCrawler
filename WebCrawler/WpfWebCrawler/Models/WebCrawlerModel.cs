using System.Collections.Specialized;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebCrawler.Contracts.OutputModels;
using WebCrawler.Contracts.Services;
using WebCrawler.Services;


namespace WpfWebCrawler.Models
{
    internal class WebCrawlerModel
    {
        #region Public Methods

        public Task<ICrawlResult> GetCrawlResultAsync()
        {
            return Task.Run(async () =>
            {
                var regex = new Regex(@"\s+");
                var appSettings = GetRefreshedSettings();
                var searchDepth = int.Parse(appSettings["SearchDepth"]);
                var rootUrls = regex.Split(appSettings["RootUrls"]);
                var concurrencyLevel = int.Parse(appSettings["ConcurrencyLevel"]);
                using (IWebCrawlerService webCrawler = new WebCrawlerService(searchDepth, concurrencyLevel))
                {
                    return await webCrawler.PerformCrawlingAsync(rootUrls);
                }
            });
        }

        #endregion

        #region Private Methods

        private NameValueCollection GetRefreshedSettings()
        {
            ConfigurationManager.RefreshSection("appSettings");
            return ConfigurationManager.AppSettings;
        }

        #endregion
    }
}
