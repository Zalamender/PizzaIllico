using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PizzaIllico.Mobile.Dtos.Accounts;
using PizzaIllico.Mobile.ViewModels;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.Services
{
    public interface IApiService
    {
        Task<TResponse> Get<TResponse>(string url);
        Task<TResponse> Post<TResponse>(string url, Object user);
        Task<TResponse> Patch<TResponse>(string url, Object obj);
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, HOST + url);

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
            HttpRequestMessage request = new HttpRequestMessage(method, HOST + url)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"),
              
            };

            request.Headers.Add("Authorization", "Bearer "+"fd71ac8943384d158a662a393919d2b6");
            Console.WriteLine("requete"+request);
            HttpResponseMessage response = await _client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(content);
        }
    }
}