using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlingLib
{
    public class CrawlingResult
    {
        public string RootURL { get; set; }

        private readonly List<CrawlingResult> children;

        public List<CrawlingResult> Children
        {
            get
            {
                return children;
            }
        }

        public CrawlingResult() : this(null, null) { }

        public CrawlingResult(string URL) 
            : this(URL, null) { }

        public CrawlingResult(string URL, IEnumerable<CrawlingResult> subResults)
        {
            children = subResults != null
                ? new List<CrawlingResult>(subResults)
                : new List<CrawlingResult>();
            this.RootURL = URL != null ? URL : string.Empty;
        }

    }
}
