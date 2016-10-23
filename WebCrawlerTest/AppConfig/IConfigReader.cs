
namespace WebCrawlerTest.AppConfig
{
    internal interface IConfigReader
    {
        ConfigData ReadApplicationConfig(string filePath);
    }
}
