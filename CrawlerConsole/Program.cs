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

namespace CrawlerConsole
{
    class Program
    {
        public static List<Tuple<string, string>> CnnHeadlinesAndLinks = new List<Tuple<string, string>>();
        public static string _twitterconsumerKey = "k7nEERRqWabOaQf6hWfMU2PLm"; // Your consumer key
        public static string _twitterconsumerSecret = "vH8JHbshHLe7pjykFqtwpTcQnf01LkHTkfkBAth81ng3IYymM0"; // Your _consumerSecret key  
        public static string _twitteraccessToken = "1344081889-Emx0sqgFdIUNZd2hF9kCXGfz8jOfUMUAuf1LCZa"; // Your _accessToken key
        public static string _twitteraccessTokenSecret = "yFr6fWRP5POpq7Bpd3Am7NoSHVxvUyMeyLDNVEBRvj4c1"; // Your _accessTokenSecret key 
        public static List<TwitterStatus> twitterObjects = new List<TwitterStatus>();
        static void Main(string[] args)
        {
            List<Thread> threadList = new List<Thread>();
            Thread t1 = new Thread(() => StartCNNCrawler());
            Thread t2 = new Thread(() => StartTwitterCrawler());
            //threadList.Add(t1);
            //threadList.Add(t2);
            //threadList.ForEach(x => x.Start());
            t1.Start();
            t2.Start();
            Console.ReadLine();
        }

        public static void StartCNNCrawler()
        {
            try
            {
                Console.WriteLine("starting CNN Crawling");
                var url = "https://search.api.cnn.io/content?q=trump&size=25&type=article"; //CNN's search API for articles. Search Parameter gets first 25 articles
                //var httpClient = new HttpClient();
                //var html = await httpClient.GetStringAsync(url);
                //var htmlDocument = new HtmlDocument();
                //htmlDocument.LoadHtml(html);
                //node.GetAttributeValue("class", "").Equals("cnn-search__results-list")).ToList();

                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine(response.StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                dynamic cnnApiJsonObject = JValue.Parse(responseFromServer);
                List<Tuple<string, string>> theHeadlinesAndLinks = AddLinksAndHeadlinesToTuple(cnnApiJsonObject);
                //StreamWriter stream = new StreamWriter(@"C:\Users\App-Sope\Documents\Personal\Crawler\CnnCrawlerTxt.txt", true);
                #region Testing that the appropriate items are retrieved from CNN's API
                //foreach (var tupleItem in theHeadlinesAndLinks)
                //{

                //    string write = string.Format("Headline :{0} || Link :{1}", tupleItem.Item1, tupleItem.Item2);
                //    stream.WriteLine(write);
                //}
                //stream.Close();
                //System.IO.File.WriteAllText(@"C:\Users\App-Sope\Documents\Personal\Crawler\CrawlerTxt.txt", stream);
                #endregion
                //CNN_API apiResultsList = (CNN_API)Newtonsoft.Json.JsonConvert.DeserializeObject(responseFromServer, typeof(CNN_API));
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                StreamWriter stream = new StreamWriter(@"C:\Users\App-Sope\Documents\Personal\Crawler\Exceptions.txt", true);
                string themessage = ex.GetBaseException().ToString();
                stream.WriteLine(themessage);
                stream.Close();
                throw;
            }
            finally
            {
                Console.WriteLine("completed CNN Crawling");
            }   
        }

        public static async void StartTwitterCrawler()
        {
            bool didExceptionOccur = false;
            Console.WriteLine("starting Twitter Crawling");
            TwitterService twitterService = new TwitterService(_twitterconsumerKey, _twitterconsumerSecret);
            twitterService.AuthenticateWith(_twitteraccessToken, _twitteraccessTokenSecret);

            var url = "https://twitter.com/search?q=trump&src=typd"; //Twitter's search API for articles. Make a web request call to this search url, so as to ensure it's up and running. If this isn't done, it throws an "Arithmetic Overflow" exception everytime the url is not running
            WebRequest request = WebRequest.CreateHttp(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();            
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            //var httpClient = new HttpClient();
            //var html = await httpClient.GetStringAsync(url);
            //var htmlDocument = new HtmlDocument();
            //htmlDocument.LoadHtml(html);
            //node.GetAttributeValue("class", "").Equals("cnn-search__results-list")).ToList();

            StreamWriter stream2 = new StreamWriter(@"C:\Users\App-Sope\Documents\Personal\Crawler\TwitterData.txt", true);
            stream2.Write(responseFromServer);
            stream2.Close();

            try
            {               
                int tweetcount = 1;
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
                //List<TwitterStatus> resultList = new List<TwitterStatus>(tweets_search.Statuses);
                //List<TwitterStatus> resultList = new List<TwitterStatus>(tweetsToDisplay);
                if (tweetsToDisplay != null && tweetsToDisplay.Count() > 0)
                {
                    twitterObjects = tweetsToDisplay.ToList();
                    foreach (var tweet in tweetsToDisplay)
                    {                        
                        //StreamWriter stream = new StreamWriter(@"C:\Users\App-Sope\Documents\Personal\Crawler\TwitterCrawlerTxt.txt", true);
                        try
                        {
                                                       
                            //stream.WriteLine("Writing Properties for tweet entity " + tweetcount);
                            //foreach (var property in tweet.GetType().GetProperties())
                            //{
                            //    #region Testing that the appropriate items are retrieved from Twitter's API
                                
                            //    StringBuilder stringForEntity = new StringBuilder();
                            //    stringForEntity.AppendLine(string.Format("{0}: val_{1}", (property != null) ? property.Name : "No Property Name", (property.GetValue(tweet) != null)?property.GetValue(tweet).ToString() :"No Property Value"));
                            //    stream.WriteLine(stringForEntity);                                
                            //    //System.IO.File.WriteAllText(@"C:\Users\App-Sope\Documents\Personal\Crawler\CrawlerTxt.txt", stream);
                            //    #endregion
                            //}
                            //stream.Close();



                            //tweet.User.ScreenName;  
                            //tweet.User.Name;   
                            //tweet.Text; // Tweet text  
                            //tweet.RetweetCount; //No of retweet on twitter  
                            //tweet.User.FavouritesCount; //No of Fav mark on twitter  
                            //tweet.User.ProfileImageUrl; //Profile Image of Tweet  
                            //tweet.CreatedDate; //For Tweet posted time  
                            //"https://twitter.com/intent/retweet?tweet_id=" + tweet.Id;  //For Retweet  
                            //"https://twitter.com/intent/tweet?in_reply_to=" + tweet.Id; //For Reply  
                            //"https://twitter.com/intent/favorite?tweet_id=" + tweet.Id; //For Favorite  

                            //Above are the things we can also get using TweetSharp.  
                            Console.WriteLine("Sr.No: " + tweetcount + "\n" + tweet.User.Name + "\n" + tweet.User.ScreenName + "\n" + "https://twitter.com/intent/retweet?tweet_id=" + tweet.Id);
                            tweetcount++;
                        }
                        catch (Exception ex)
                        {
                            //stream.Dispose();
                            //throw;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                StreamWriter stream = new StreamWriter(@"C:\Users\App-Sope\Documents\Personal\Crawler\Exceptions.txt", true);
                string themessage = ex.GetBaseException().ToString();
                stream.WriteLine(themessage);
                stream.Close();
                //throw;
            }
            finally
            {
                Console.WriteLine("Completed Twitter Crawling");
            }           
            
        }

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
