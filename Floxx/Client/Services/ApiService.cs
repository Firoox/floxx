using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Floxx.Client.Interfaces;
using Newtonsoft.Json;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

namespace Floxx.Client.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string path, TRequest data)
        {
            try
            { 
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent serialized = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PostAsync(path, serialized))
                {
                    //response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TResponse>(responseBody);
                }
            }
            catch (Exception ex)
            {
                return default(TResponse);
            }
        }

        public async Task<string> GetStringAsync(string path)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await httpClient.GetAsync(path))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TResponse> GetJsonAsync<TResponse>(string path, Dictionary<string, string> parameters = null)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await httpClient.GetAsync(path))
                {
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TResponse>(responseBody);
                }
            }
            catch (Exception ex)
            {
                return default(TResponse);
            }
        }

        public async Task<TResponse> PutAsync<TResponse>(string path)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await httpClient.PutAsync(path, new StringContent(string.Empty)))
                {
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TResponse>(responseBody);
                }
            }
            catch (Exception ex)
            {
                return default(TResponse);
            }
        }

        public async Task<TResponse> PutJsonAsync<TRequest, TResponse>(string path, TRequest data)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent serialized = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PutAsync(path, serialized))
                {
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TResponse>(responseBody);
                }
            }
            catch (Exception ex)
            {
                return default(TResponse);
            }
        }

        public async Task<string> DeleteJsonAsync(string path)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await httpClient.DeleteAsync(path))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
