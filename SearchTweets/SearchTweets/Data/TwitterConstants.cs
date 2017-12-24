using System;
using System.Text;

namespace SearchTweets.Data
{
    internal static class TwitterConstants
    {
        internal static string OauthConsumerKey = "4wEbnTDFyBycamjKOGKcGpId3";
        internal static string OauthConsumerSecret = "FLvE7ih4XW3HBikMRUHZIdcqELPpxQ7yFNSkzw2zhxtKpglVSh";
        internal static string OauthAccessToken = "944898246551506945-QoEu945OIptRWM4r3Vhcx46XbZxeTSr";
        internal static string OauthAccessTokenSecret = "lQPdAySxYu6b1PnEuuu6408vwyObItNp1mbaHOYxxL5KE";
        internal static string OauthVersion = "1.0";
        internal static string OauthSignatureMethod = "HMAC-SHA1";
        internal static string OauthNonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        internal static string OauthTimestamp = ((int)TimeSpan.TotalSeconds).ToString();
        internal static string StandardSearchApiUrl = "https://api.twitter.com/1.1/search/tweets.json";

        private static TimeSpan TimeSpan
        {
            get
            {
                return DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            }
        }
    }
}
