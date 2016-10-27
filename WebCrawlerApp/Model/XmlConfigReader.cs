using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace WebCrawlerApp.Model
{
    class XmlConfigReader : IConfigProvider
    {
        private readonly string _filename;
        private IList<string> _rootUrls;
        private int _nestingDepth;
        private bool _needLoad;

        public IEnumerable<string> RootUrls
        {
            get
            {
                LoadConfig();
                return new ReadOnlyCollection<string>(_rootUrls);
            }
        }

        public int NestingDepth
        {
            get
            {
                LoadConfig();
                return _nestingDepth;
            }
            private set
            {
                _nestingDepth = value;
            }
        }

        public XmlConfigReader(string filename)
        {
            _filename = filename;
            _rootUrls = new List<string>();
            NestingDepth = 0;
            _needLoad = true;
        }

        // Internals

        private void LoadConfig()
        {
            if (_needLoad)
            {
                try
                {
                    XDocument document = XDocument.Load(_filename);
                    XElement root = document.Root;

                    NestingDepth = int.Parse(root.Element("depth").Value);
                    _rootUrls =
                        root.Element("rootResources")
                            .Elements("resource")
                            .Select(x => x.Value)
                            .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                    _needLoad = false;
                }
                catch (Exception e)
                {
                    throw new ConfigLoadingException("Error while config loading", e);
                }
            }
        }
    }
}
