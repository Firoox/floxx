using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floxx.Client.Interfaces
{
    public interface IApiService
    {
        Task<TResponse> PostJsonAsync<TRequest, TResponse>(string path, TRequest data);

        Task<string> GetStringAsync(string path);

        Task<TResponse> GetJsonAsync<TResponse>(string path, Dictionary<string, string> parameters = null);

        Task<TResponse> PutAsync<TResponse>(string path);

        Task<TResponse> PutJsonAsync<TRequest, TResponse>(string path, TRequest data);

        Task<string> DeleteJsonAsync(string path);
    }
}
