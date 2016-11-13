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

        private readonly LinkedList<CrawlingResult> children;

        public LinkedList<CrawlingResult> Children
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
                ? new LinkedList<CrawlingResult>(subResults)
                : new LinkedList<CrawlingResult>();
            this.RootURL = URL != null ? URL : string.Empty;
        }

    }
}
