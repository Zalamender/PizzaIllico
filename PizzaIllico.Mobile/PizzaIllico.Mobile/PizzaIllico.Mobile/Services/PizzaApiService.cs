using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Accounts;
using PizzaIllico.Mobile.Dtos.Authentications;
using PizzaIllico.Mobile.Dtos.Authentications.Credentials;
using PizzaIllico.Mobile.Dtos.Pizzas;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.Services
{
    public interface IPizzaApiService
    {
        Task<Response<List<ShopItem>>> ListShops();

        Task<Response<List<PizzaItem>>> ListPizzas(int shopId);

        Task<Response<string>> ImagePizza(int shopId, int pizzaId);

        Task<Response<LoginResponse>> Register(CreateUserRequest user);
        Task<Response<LoginResponse>> Login(LoginWithCredentialsRequest user);
        Task<Response> SetPassword(SetPasswordRequest password);
    }
    
    public class PizzaApiService : IPizzaApiService
    {
        private readonly IApiService _apiService;

        public PizzaApiService()
        {
            _apiService = DependencyService.Get<IApiService>();
        }

        public async Task<Response<List<ShopItem>>> ListShops()
        {
	        return await _apiService.Get<Response<List<ShopItem>>>(Urls.LIST_SHOPS);
        }

        public async Task<Response<List<PizzaItem>>> ListPizzas(int shopId)
        {
            string str = "{shopId}";
            return await _apiService.Get<Response<List<PizzaItem>>>(Urls.LIST_PIZZA.Replace(str, shopId.ToString()));
        }

        public async Task<Response<string>> ImagePizza(int shopId, int pizzaId)
        {
            string str1 = "{shopId}";
            string str2 = "{pizzaId}";
            Console.WriteLine($"Requete effectuée :" + Urls.GET_IMAGE.Replace(str1, shopId.ToString()).Replace(str2, pizzaId.ToString()));
            return await _apiService.Get<Response<string>>(Urls.GET_IMAGE.Replace(str1, shopId.ToString()).Replace(str2, pizzaId.ToString()));
        }
       
        public async Task<Response<LoginResponse>> Register(CreateUserRequest user)
        {
            return await _apiService.Post<Response<LoginResponse>>(Urls.CREATE_USER,user);
        }

        public async Task<Response<LoginResponse>> Login(LoginWithCredentialsRequest user)
        {
            return await _apiService.Post<Response<LoginResponse>>(Urls.LOGIN_WITH_CREDENTIALS, user);
        }
        
        public async Task<Response> SetPassword(SetPasswordRequest password)
        {
            return await _apiService.Patch<Response>(Urls.SET_PASSWORD, password);
        }
    }
}