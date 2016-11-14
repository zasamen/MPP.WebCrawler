﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MPP.WebCrawler.Model
{
    internal class ConfigurationReader
    {
        private string[] urlsToCrawl;
        private string filename;

        private int ValidateNestingDepth(string depth)
        {
            int result;
            return int.TryParse(depth, out result) ? result : 0;
        }

        private string[] ValidateRoots(IEnumerable<string> roots)
        {
            return roots.Where(x => !(string.IsNullOrWhiteSpace(x) 
            || string.IsNullOrEmpty(x))).ToArray();         
        }

        private bool isLoaded = false;

        private int nestingDepth;

        private void LoadIfNotLoaded()
        {
            if (!isLoaded)
            {
                LoadConfiguration();
                isLoaded = true;
            }
        }
        private void LoadConfiguration()
        {
            try
            {
                var root = XDocument.Load(filename).Root;
                NestingDepth = ValidateNestingDepth(root.Element("depth").Value);
                urlsToCrawl = ValidateRoots(root.
                    Element("rootResources").
                    Elements("resource").
                    Select(o => o.Value));
            }
            catch (Exception e)
            {
                throw new ConfigurationReadingException(
                    "Can't read configuration file");
            }
        }

        internal int NestingDepth
        {
            get
            {
                LoadIfNotLoaded();
                return nestingDepth;
            }
            private set
            {
                nestingDepth = value;
            }
        }
        internal string[] UrlsToCrawl
        {
            get
            {
                LoadIfNotLoaded();
                return urlsToCrawl;
            }
        }

        internal string FileName
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }

        internal ConfigurationReader(string filename)
        {
            this.filename = filename;
        }
    }
}
