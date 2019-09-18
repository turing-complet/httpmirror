using System.Collections.Generic;

namespace Mirror
{
    public static class Helper
    {
        // parse output of Uri.Query property
        // e.g.
        // ?q=foo&token=hjkl
        public static Dictionary<string, string> ParseQuery(string query)
        {
            var result = new Dictionary<string, string>();
            query = query.Substring(1);
            string[] parts = query.Split('&');
            foreach (string kv in parts)
            {
                string[] pair = kv.Split('=');
                result.Add(pair[0], pair[1]);
            }
            return result;
        }
    }
}