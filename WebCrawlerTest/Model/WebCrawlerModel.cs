using System;
using System.Threading.Tasks;
using WebCrawlerLib.WebCrawler;
using WebCrawlerTest.AppConfig;

namespace WebCrawlerTest.Model
{
    internal class WebCrawlerModel : IConfigurable
    {
        private const string AppConfigFilePath = "..\\..\\AppConfig\\config.xml";

        private ISimpleWebCrawler webCrawler;
        public ConfigData ConfigData{ get; set;}

        public WebCrawlerModel()
        {
            webCrawler = new WebCrawler();
            IConfigReader configReader = new XmlConfigReader();
            ConfigData = LoadApplicationConfig(configReader);
        }

        public Task<CrawlResult> GetWebCrawlingResultAsync()
        {
            return webCrawler.PerformCrawlingAsync(ConfigData.Depth, ConfigData.RootResources);
        }

        public ConfigData LoadApplicationConfig(IConfigReader configReader)
        {
            return configReader.ReadApplicationConfig(AppConfigFilePath);
        }
    }
}
