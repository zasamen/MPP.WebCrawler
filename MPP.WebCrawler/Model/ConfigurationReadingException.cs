using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MPP.WebCrawler.Model
{
    [Serializable]
    internal class ConfigurationReadingException : Exception
    {
        public ConfigurationReadingException() :base()
        {
        }

        public ConfigurationReadingException(string message) : base(message)
        {
        }

        public ConfigurationReadingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationReadingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
