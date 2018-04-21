using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using HtmlAgilityPack;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using TweetSharp;
using System.Web;

namespace CNNTwitterCrawler.Base
{
    public class CrawlerLauncher
    {
        public static List<Tuple<string, string>> CnnHeadlinesAndLinks = new List<Tuple<string, string>>();
        public static string _twitterconsumerKey = System.Configuration.ConfigurationSettings.AppSettings["_twitterconsumerKey"]; // Your consumer key
        public static string _twitterconsumerSecret = System.Configuration.ConfigurationSettings.AppSettings["_twitterconsumerSecret"]; // Your _consumerSecret key  
        public static string _twitteraccessToken = System.Configuration.ConfigurationSettings.AppSettings["_twitteraccessToken"]; // Your _accessToken key
        public static string _twitteraccessTokenSecret = System.Configuration.ConfigurationSettings.AppSettings["_twitteraccessTokenSecret"]; // Your _accessTokenSecret key 
        public static List<TwitterStatus> twitterObjects = new List<TwitterStatus>();
        public static List<Tuple<string, string>> StartCNNCrawler()
        {
            List<Tuple<string, string>> theHeadlinesAndLinks = new List<Tuple<string, string>>();
            try
            {
                Console.WriteLine("starting CNN Crawling");
                var url = "https://search.api.cnn.io/content?q=trump&size=25&type=article"; //CNN's search API for articles. Search Parameter gets first 25 articles
                //API was gotten after analyzing the page source of the results page for a seach query I made on CNN's website 

                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine(response.StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                //Parse response from CNN API to JSON format
                dynamic cnnApiJsonObject = JValue.Parse(responseFromServer);
                //Add Headlines and corresponding links for each article to a Tuple
                theHeadlinesAndLinks = AddLinksAndHeadlinesToTuple(cnnApiJsonObject);
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                //StreamWriter stream = new StreamWriter(@"C:\Users\App-Sope\Documents\Personal\Crawler\Exceptions.txt", true);
                //string themessage = ex.GetBaseException().ToString();
                //stream.WriteLine(themessage);
                //stream.Close();
                //throw;
            }
            finally
            {
                Console.WriteLine("completed CNN Crawling");
            }
            return theHeadlinesAndLinks;
        }

        public static List<TwitterStatus> StartTwitterCrawler()
        {
            bool didExceptionOccur = false;
            Console.WriteLine("starting Twitter Crawling");
            TwitterService twitterService = new TwitterService(_twitterconsumerKey, _twitterconsumerSecret);
            twitterService.AuthenticateWith(_twitteraccessToken, _twitteraccessTokenSecret);

            var url = "https://twitter.com/search?q=trump&src=typd"; //Twitter's search API for articles. Make a web request call to this search url, so as to ensure it's up and running. If this isn't done, it throws an "Arithmetic Overflow" exception everytime the url is not running
            //I did this because I noticed that the API call always worked fine whenever I refreshed the my browser twitter window containing the above search url.
            WebRequest request = WebRequest.CreateHttp(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            try
            {
                var tweets_search = new TwitterSearchResult();
                if (!didExceptionOccur)
                {
                    try
                    {
                        tweets_search = twitterService.Search(new SearchOptions { Q = "%23Trump", Count = 25 }); //Note '%23' is twitter's notation for HashTag. You can try searching for '#Trump' and watch the search notation change to '%23Trump'
                    }
                    catch
                    {
                        didExceptionOccur = true;
                    }

                }
                if (didExceptionOccur)
                {
                    //set the web request again, to simulate refreshing browser page, before calling the tweetsearch api
                    WebRequest request2 = WebRequest.CreateHttp(url);
                    request2.Credentials = CredentialCache.DefaultCredentials;
                    HttpWebResponse response2 = (HttpWebResponse)request.GetResponse();
                    Stream dataStream2 = response.GetResponseStream();
                    StreamReader reader2 = new StreamReader(dataStream);
                    string responseFromServer2 = reader.ReadToEnd();
                    tweets_search = twitterService.Search(new SearchOptions { Q = "%23Trump" }); //Do not use 'Count' property, as exception already caught and returned meant that the entities returned didn't match the count parameter
                }
                var tweetsToDisplay = new TwitterSearchResult().Statuses;
                if (tweets_search != null && tweets_search.Statuses != null && tweets_search.Statuses.Count() > 25)
                {
                    tweetsToDisplay = tweets_search.Statuses.Take(25);
                }
                else if (tweets_search != null && tweets_search.Statuses != null && tweets_search.Statuses.Count() > 0 && tweets_search.Statuses.Count() <= 25)
                {
                    tweetsToDisplay = tweets_search.Statuses;
                }
                else
                {
                    tweetsToDisplay = null;
                }
                if (tweetsToDisplay != null && tweetsToDisplay.Count() > 0)
                {
                    twitterObjects = tweetsToDisplay.ToList();
                }

            }
            catch (Exception ex)
            {
                //StreamWriter stream = new StreamWriter(@"C:\Users\App-Sope\Documents\Personal\Crawler\Exceptions.txt", true);
                //string themessage = ex.GetBaseException().ToString();
                //stream.WriteLine(themessage);
                //stream.Close();
                //throw;
            }
            finally
            {
                Console.WriteLine("Completed Twitter Crawling");
            }
            return twitterObjects;
        }

        //Add Headlines and corresponding links for each article to a Tuple
        public static List<Tuple<string, string>> AddLinksAndHeadlinesToTuple(dynamic jsonObject)
        {
            string headline = string.Empty;
            string link = string.Empty;
            Tuple<string, string> tupleToAdd = new Tuple<string, string>(headline, link);
            foreach (var item in jsonObject.result)
            {
                headline = item.headline;
                link = item.url;
                tupleToAdd = new Tuple<string, string>(headline, link);                
                CnnHeadlinesAndLinks.Add(tupleToAdd);
            }
            return CnnHeadlinesAndLinks;
        }
    }
}
