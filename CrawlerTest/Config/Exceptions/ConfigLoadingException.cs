using System;
using System.Runtime.Serialization;

namespace CrawlerTest.Config.Exceptions
{
    [Serializable]
    internal class ConfigLoadingException : Exception
    {
        private Exception ex;

        public ConfigLoadingException()
        {
        }

        public ConfigLoadingException(string message) : base(message)
        {
        }

        public ConfigLoadingException(Exception ex)
        {
            this.ex = ex;
        }

        public ConfigLoadingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigLoadingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}