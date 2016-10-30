namespace WebCrawlerTest.AppConfig
{
    class ConfigData
    {
        public int Depth { get; set; }
        public string[] RootResources { get; set; }

        public ConfigData()
        {
            RootResources = new string[0];
        }
        
    }
}
