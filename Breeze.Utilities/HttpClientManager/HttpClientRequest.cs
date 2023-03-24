using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Utilities.HttpClientManager
{
    public class HttpClientRequest<T> : IHttpClientRequest<T> where T : class
    {
        private readonly HttpClient _httpClient;

        public HttpClientRequest(string baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public async Task<T> GetAsync(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode) return default;

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string requestUri, TRequest requestData)
        {
            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUri, content);
            if (!response.IsSuccessStatusCode) return default;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }
    }
}
