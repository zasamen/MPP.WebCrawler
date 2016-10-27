using System.Threading.Tasks;
using WebCrawlerLib.WebCrawler;
using WebCrawlerTest.AppConfig;

namespace WebCrawlerTest.Model
{
    internal class WebCrawlerModel : IConfigurable
    {
        private const string AppConfigFilePath = "..\\..\\AppConfig\\config.xml";
        public ConfigData ConfigData{ get; set;}

        public WebCrawlerModel()
        {
            
            IConfigReader configReader = new XmlConfigReader();
            ConfigData = LoadApplicationConfig(configReader);
        }

        public Task<CrawlResult> GetWebCrawlingResultAsync()
        {
            using (WebCrawler webCrawler = new WebCrawler())
            {
                return webCrawler.PerformCrawlingAsync(ConfigData.Depth, ConfigData.RootResources);
            }
        }

        public ConfigData LoadApplicationConfig(IConfigReader configReader)
        {
            return configReader.ReadApplicationConfig(AppConfigFilePath);
        }
    }
}
