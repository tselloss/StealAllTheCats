using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Extensions.HttpClient
{
    public class HttpHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CatApiSettings _settings;

        public HttpHelper(IHttpClientFactory httpClientFactory, IOptions<CatApiSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _settings = options.Value;
        }

        public async Task<string> GetContent()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);

            var response = await client.GetAsync(_settings.BaseUrl);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
