using System;
using System.Runtime.Serialization;

namespace WebCrawlerApp.Model
{
    [Serializable]
    internal class ConfigLoadingException : Exception
    {
        public ConfigLoadingException()
        {
        }

        public ConfigLoadingException(string message) : base(message)
        {
        }

        public ConfigLoadingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigLoadingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}