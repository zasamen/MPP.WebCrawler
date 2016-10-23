namespace WebCrawlerTest.AppConfig
{
    internal interface IConfigurable
    {
        ConfigData ConfigData { get; set; }
        ConfigData LoadApplicationConfig(IConfigReader configReader);
    }

}
