using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNNTwitterCrawler.Base
{
    public class CNN_API
    {
        public List<results> theResults
        {
            get; set;
        }
        public meta theMeta
        {
            get; set;
        }
    }

    public class results
    {
        public string _id
        {
            get; set;
        }
        public string type
        {
            get; set;
        }
        public string sourceId
        {
            get; set;
        }
        public string url
        {
            get; set;
        }
        public string source
        {
            get; set;
        }
        public string location
        {
            get; set;
        }       
        public string byLine
        {
            get; set;
        }
        public List<string> contributors
        {
            get; set;
        }
        public DateTime firstPublishDate //from DateTime to string
        {
            get; set;
        }
        public DateTime lastModifiedDate //from DateTime to string
        {
            get; set;
        }
        public string headline
        {
            get; set;
        }
        public string section
        {
            get; set;
        }
        public string mappedSection
        {
            get; set;
        }
        public string body
        {
            get; set;
        }
        public string thumbnail
        {
            get; set;
        }
        public string language
        {
            get; set;
        }
        public List<string> topics
        {
            get; set;
        }
        public List<string> suggestTopics
        {
            get; set;
        }
    }

    public class meta
    {
        public string duration
        {
            get; set;
        }
        public string start
        {
            get; set;
        }
        public string end
        {
            get; set;
        }
        public string total
        {
            get; set;
        }
        public string of
        {
            get; set;
        }
        public string maxScore
        {
            get; set;
        }
    }
}
