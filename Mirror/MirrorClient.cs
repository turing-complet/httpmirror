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
                string result = Config.Enabled;
                return result != null ? result == "1" : true;
            }
        }

        private HttpClient _client;
        private IStorage _storage;

        public MirrorClient(string domain)
        {
            _storage = new BlobStorage(domain);
            _client = new HttpClient();
            _client.BaseAddress = new Uri(domain);
        }

        public async Task<string> GetAsync(string requestUri)
        {
            if (enabled && _storage.Exists(requestUri))
            {
                return _storage.Get(requestUri);
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var resp = await _client.SendAsync(request);

            string content = await resp.Content.ReadAsStringAsync();

            if (enabled)
            {
                _storage.Put(requestUri, content);
            }

            return content;
        }
    }
}
