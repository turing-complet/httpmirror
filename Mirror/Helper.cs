using System;
using System.Collections.Generic;
using System.Text;

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
            if (string.IsNullOrWhiteSpace(query))
            {
                return result;
            }
            
            query = query.Substring(1);
            string[] parts = query.Split('&');
            foreach (string kv in parts)
            {
                string[] pair = kv.Split('=');
                result.Add(pair[0], pair[1]);
            }
            return result;
        }

        public static string EncodeQuery(string query)
        {
            string result = "";
            var parsed = Helper.ParseQuery(query);
            foreach (var key in parsed.Keys)
            {
                result += $"{key};{parsed[key]}";
            }
            byte[] bytes = Encoding.UTF8.GetBytes(result);
            return Convert.ToBase64String(bytes);
        }
    }
}