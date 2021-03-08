using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Insight.Application.Interfaces;
using Newtonsoft.Json;

namespace Insight.Infrastructure.Services
{
    public class HttpService : IHttpService, IDisposable
    {
        private readonly HttpClient _httpClient;
        public Uri BaseAddress => _httpClient.BaseAddress;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<T> GetAsync<T>(string url)
        {
            T data = default(T);

            if (!string.IsNullOrEmpty(url))
            {
                WebClient client = new WebClient();
                string reply = await client.DownloadStringTaskAsync(BaseAddress + url);

                data = JsonConvert.DeserializeObject<T>(reply);

            }
            return data;
        }

        public async Task<T> PostAsync<T>(string url, object body)
        {
            string payload = JsonConvert.SerializeObject(new
            {
                postcodes = body

            });
            var cli = new WebClient();
            SetHeaderParameters(cli);
            var response = await cli.UploadStringTaskAsync(BaseAddress + url, payload);
            return JsonConvert.DeserializeObject<T>(response);

        }
        private void SetHeaderParameters(WebClient client)
        {
            client.Headers.Clear();
            client.Headers.Add("Content-Type", "application/json");
            client.Encoding = Encoding.UTF8;
        }
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
