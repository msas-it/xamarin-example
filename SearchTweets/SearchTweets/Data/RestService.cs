using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchTweets.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace SearchTweets.Data
{
    public class RestService
    {
        public static List<SearchResultItem> SearchTwittter(SearchOptions searchOptions)
        {
            var headerFormat = "OAuth oauth_consumer_key=\"{3}\",oauth_nonce=\"{0}\",oauth_signature=\"{5}\",oauth_signature_method=\"{1}\",oauth_timestamp=\"{2}\",oauth_token=\"{4}\",oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(TwitterConstants.OauthNonce),
                                    Uri.EscapeDataString(TwitterConstants.OauthSignatureMethod),
                                    Uri.EscapeDataString(TwitterConstants.OauthTimestamp),
                                    Uri.EscapeDataString(TwitterConstants.OauthConsumerKey),
                                    Uri.EscapeDataString(TwitterConstants.OauthAccessToken),
                                    Uri.EscapeDataString(GetTwitterOauthSignature(searchOptions)),
                                    Uri.EscapeDataString(TwitterConstants.OauthVersion)
                            );
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", authHeader);
                var queryString = string.Format("q={0}", Uri.EscapeDataString(searchOptions.SearchQuery));
                var task = httpClient.GetAsync(TwitterConstants.StandardSearchApiUrl + "?" + queryString)
                    .ContinueWith(response =>
                    {
                        var result = new List<SearchResultItem>();
                        var responseResult = response.Result;
                        var stringResult = responseResult.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<JObject>(stringResult);
                        var jsonstatuses = (JArray)jsonResult["statuses"];
                        foreach (var item in jsonstatuses)
                        {
                            result.Add(new SearchResultItem
                            {
                                Photo = item["user"]["profile_image_url"].Value<string>(),
                                Title = item["text"].Value<string>()
                            });
                        }
                        return result;

                    });

                task.Wait();
                return task.Result;
            }



        }

        private static string GetTwitterOauthSignature(SearchOptions searchOptions)
        {
            var urlQueryString = string.Format("oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&q={6}",
                            TwitterConstants.OauthConsumerKey,
                            TwitterConstants.OauthNonce,
                            TwitterConstants.OauthSignatureMethod,
                            TwitterConstants.OauthTimestamp,
                            TwitterConstants.OauthAccessToken,
                            TwitterConstants.OauthVersion,
                            Uri.EscapeDataString(searchOptions.SearchQuery)
                            );

            var stringUrl = string.Concat("GET&", Uri.EscapeDataString(TwitterConstants.StandardSearchApiUrl), "&", Uri.EscapeDataString(urlQueryString));

            var compositeKey = string.Concat(Uri.EscapeDataString(TwitterConstants.OauthConsumerSecret), "&", Uri.EscapeDataString(TwitterConstants.OauthAccessTokenSecret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(stringUrl)));
            }

            return oauth_signature;
        }


    }
}
