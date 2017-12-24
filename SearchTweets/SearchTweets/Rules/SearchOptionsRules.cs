using System;

namespace SearchTweets.Rules
{
    public class SearchOptionsRules
    {
        public static bool Validate(string query)
        {
            return !String.IsNullOrEmpty(query);
        }
    }
}
