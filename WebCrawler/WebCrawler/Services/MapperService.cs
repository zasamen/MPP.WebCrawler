using WebCrawler.Contracts.Models;
using System;
using WebCrawler.Models;
using System.Collections.Generic;
using WebCrawler.Contracts.Services;

namespace WebCrawler.Services
{
    internal class MapperService: IMapperService
    {
        #region Internal Members

        public T Map<T> (string url, int nestingLevel, IEnumerable<ICrawlNode> nodes)
        {
            if (typeof(T) == typeof(ICrawlNode))
            {
                return (T)(object)new CrawlNode
                {
                    Url = url,
                    LevelDescription = (nestingLevel == 0) ? "Root node" : string.Format("Level {0}",nestingLevel),
                    InternalNodes = nodes
                };
            }
            else
                throw new ArgumentException(string.Format("Type {0} not found",typeof(T)));
        }

        public T Map<T>(IEnumerable<ICrawlNode> nodeList)
        {
            if (typeof(T) == typeof(ICrawlResult))
            {
                return (T)(object)new CrawlResult
                {
                    RootNodes = nodeList
                };
            }
            else
                throw new ArgumentException(string.Format("Type {0} not found", typeof(T)));
        }

        #endregion
    }
}
