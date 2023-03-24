using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Utilities.HttpClientManager
{
    public interface IHttpClientRequest<T>
    {
        Task<T> GetAsync(string requestUri);
        Task<TResponse> PostAsync<TRequest, TResponse>(string requestUri, TRequest requestData);
    }
}
