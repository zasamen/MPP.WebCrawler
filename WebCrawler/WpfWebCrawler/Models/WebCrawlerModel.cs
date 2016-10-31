using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WebCrawler.Contracts.OutputModels;
using WebCrawler.Contracts.Services;
using WebCrawler.Services;


namespace WpfWebCrawler.Models
{
    internal class WebCrawlerModel
    {
        #region Public Methods

        public async Task<ICrawlResult> GetCrawlResultAsync()
        {
            try
            {
                ConfigurationManager.RefreshSection("appSettings");
                int searchDepth = int.Parse(ConfigurationManager.AppSettings["SearchDepth"]);
                Regex regex = new Regex(@"\s+");
                string[] rootUrls = regex.Split(ConfigurationManager.AppSettings["RootUrls"]);
                using (IWebCrawlerService _webCrawler = new WebCrawlerService(searchDepth))
                {
                    return await _webCrawler.PerformCrawlingAsync(rootUrls);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #endregion
    }
}
