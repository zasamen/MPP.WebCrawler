using WebCrawler.Contracts.Models;
using System;
using WebCrawler.Models;
using System.Collections.Generic;
using WebCrawler.Contracts.Services;

namespace WebCrawler.Services
{
    internal class Mapper: IMapper
    {
        #region Internal Members

        public T Map<T> (string url, int nestingLevel, IEnumerable<ICrawlerNode> nodes)
        {
            if (typeof(T) == typeof(ICrawlerNode))
            {
                return (T)(object)new CrawlerNode
                {
                    Url = url,
                    LevelDescription = (nestingLevel == 0) ? "Root node" : string.Format("Level {0}",nestingLevel),
                    InternalNodes = nodes
                };
            }
            else
                throw new ArgumentException(string.Format("Type {0} not found",typeof(T)));
        }

        public T Map<T>(IEnumerable<ICrawlerNode> nodeList)
        {
            if (typeof(T) == typeof(ICrawlerResult))
            {
                return (T)(object)new CrawlerResult
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
