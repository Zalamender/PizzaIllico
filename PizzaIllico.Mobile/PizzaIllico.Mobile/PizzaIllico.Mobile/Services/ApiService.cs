using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PizzaIllico.Mobile.ViewModels;

namespace PizzaIllico.Mobile.Services
{
    public interface IApiService
    {
        Task<TResponse> Get<TResponse>(string url);
        Task<TResponse> Post<TResponse>(string url, Object user);
        Task<TResponse> Patch<TResponse>(string url, Object obj);
        Task<TResponse> GetLogged<TResponse>(string url);
        Task<TResponse> PostLogged<TResponse>(string url, Object obj);
    }
    
    public class ApiService : IApiService
    {
	    private const string HOST = "https://pizza.julienmialon.ovh/";
        private readonly HttpClient _client = new HttpClient();
        
        public async Task<TResponse> Get<TResponse>(string url)
        {
	        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, HOST + url);

	        HttpResponseMessage response = await _client.SendAsync(request);

	        string content = await response.Content.ReadAsStringAsync();

	        return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task<TResponse> Post<TResponse>(string url, Object user)
        {         
            string json = JsonConvert.SerializeObject(user);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(HOST + url, httpContent);

            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task<TResponse> Patch<TResponse>(string url, Object obj)
        {
            var method = new HttpMethod("PATCH");
            string json = JsonConvert.SerializeObject(obj);

            HttpRequestMessage request = new(method, HOST + url)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"),
            };
            request.Headers.Add("Authorization", "Bearer "+User.getInstance().getAccessToken());
            Console.WriteLine("requete"+request);
            HttpResponseMessage response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content);
        }
        public async Task<TResponse> GetLogged<TResponse>(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, HOST + url);
            request.Headers.Add("Authorization", "Bearer " + User.getInstance().getAccessToken());
            HttpResponseMessage response = await _client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task<TResponse> PostLogged<TResponse>(string url, Object obj)
        {
            var method = new HttpMethod("POST");
            string json = JsonConvert.SerializeObject(obj);

            HttpRequestMessage request = new(method, HOST + url)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"),
            };
            request.Headers.Add("Authorization", "Bearer " + User.getInstance().getAccessToken());

            HttpResponseMessage response = await _client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(content);
        }

    }
}