using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mirror
{
    public class MirrorClient
    {
        private bool enabled
        {
            get
            {
                string result = Environment.GetEnvironmentVariable("ENABLE_MIRROR");
                return result != null ? result == "1" : true;
            }
        }

        private HttpClient _client;
        private IStorage _storage;

        public MirrorClient(string domain)
        {
            var baseUri = new Uri(domain);
            _storage = new BlobStorage(baseUri.Host);
            _client = new HttpClient();
            _client.BaseAddress = baseUri;
        }

        public async Task<string> GetAsync(string requestUri)
        {
            if (_storage.Exists(requestUri))
            {
                return _storage.Get(requestUri);
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var resp = await _client.SendAsync(request);

            string content = await resp.Content.ReadAsStringAsync();
            _storage.Put(requestUri, content);

            return content;
        }
    }
}
