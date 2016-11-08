using System;
using System.Collections.Generic;
using WebCrawler.Contracts.OutputModels;
using WebCrawler.OutputModels;

namespace WebCrawler.Services
{
    internal static class Mapper
    {
        #region Internal Members

        internal static T Map<T>(string url, int nestingLevel, IEnumerable<ICrawlNode> nodes)
        {
            if (typeof(T) == typeof(ICrawlNode))
                return (T)(object)new CrawlNode
                {
                    Url = url,
                    LevelDescription = (nestingLevel == 0) ? "Root node" : $"Level {nestingLevel}",
                    InternalNodes = nodes
                };
            throw new ArgumentException($"Type {typeof(T)} not found");
        }

        internal static T Map<T>(IEnumerable<ICrawlNode> nodeList)
        {
            if (typeof(T) == typeof(ICrawlResult))
                return (T)(object)new CrawlResult
                {
                    RootNodes = nodeList
                };
            throw new ArgumentException($"Type {typeof(T)} not found");
        }

        #endregion
    }
}
