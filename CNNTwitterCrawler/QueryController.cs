using CNNTwitterCrawler.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TweetSharp;

namespace CNNTwitterCrawler
{
    public class QueryController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //API Address = '/api/Query/TrumpCNNUpdatesForView'
        //DataTable that the webpage calls via above API address to display the CNN Updates gotten from "CrawlerLauncher.cs" class in "CNNTwitterCrawler.Base" assembly
        public DataTableSuccessMessage TrumpCNNUpdatesForView()
        {
            try
            {
                List<Tuple<string, string>> CnnHeadlinesAndLinks = new List<Tuple<string, string>>();
                CnnHeadlinesAndLinks = CrawlerLauncher.StartCNNCrawler();
                int querytotal = 0;
                
                var result = new DataTableSuccessMessage();                
                if (CnnHeadlinesAndLinks != null && CnnHeadlinesAndLinks.Count > 0)
                {
                    int count = 1;
                    querytotal = CnnHeadlinesAndLinks.Count;
                    result.recordsTotal = querytotal;
                    result.data = CnnHeadlinesAndLinks.Select(x => new
                    {
                        ID = count + 1,
                        HeadLine = x.Item1,
                        ViewDetails =
                          @"<button type='button' onclick=""viewDetails('" + x.Item2.Replace("www","edition") +
                          @"');"" class=""btn btn-info btn-xs"">View Article</button>", //Shows the Link upon Clicking this Button. || Replace "www" with "edition" to solve the "CORS Access denied" issue
                    }).ToArray().Take(25);
                }
                return result;
            }
            catch (Exception ex)
            {
                return new DataTableSuccessMessage()
                {
                    error = ex.Message
                };
            }                      
        }

        //API Address = '/api/Query/TrumpTwitterUpdatesForView'
        //DataTable that the webpage calls via above API address to display the Twitter Updates gotten from "CrawlerLauncher.cs" class in "CNNTwitterCrawler.Base" assembly
        public DataTableSuccessMessage TrumpTwitterUpdatesForView()
        {
            try
            {
                List<TwitterStatus> twitterObjects = new List<TwitterStatus>();
                twitterObjects = CrawlerLauncher.StartTwitterCrawler();
                int count = 1;

                var theObjects = twitterObjects.Select(x => new
                {
                    ID = count + 1,
                    TweetText = x.TextDecoded,
                    ViewDetails =
                          @"<button type='button' onclick=""viewDetails('" + ((x.RetweetedStatus != null && x.RetweetedStatus.User != null) ?x.RetweetedStatus.User.ScreenName :x.User.ScreenName) + "!!" + x.IdStr +
                          @"');"" class=""btn btn-info btn-xs"">View Tweet</button>", //Shows the Link upon Clicking this Button. 
                }).ToArray();

                if (theObjects != null && theObjects.Count() > 25)
                   theObjects = theObjects.Take(25).ToArray();

                int querytotal = 0;

                var result = new DataTableSuccessMessage();
                if (twitterObjects != null && twitterObjects.Count > 0)
                {
                    //int count = 1;
                    querytotal = twitterObjects.Count;
                    result.recordsTotal = querytotal;
                    result.data = theObjects;
                }
                return result;
            }
            catch (Exception ex)
            {
                return new DataTableSuccessMessage()
                {
                    error = ex.Message
                };
            }
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

    }

    public class DataTableSuccessMessage
    {
        public int recordsTotal
        {
            get; set;
        }
        public object data
        {
            get; set;
        }
        public string error
        {
            get; set;
        }
    }
}