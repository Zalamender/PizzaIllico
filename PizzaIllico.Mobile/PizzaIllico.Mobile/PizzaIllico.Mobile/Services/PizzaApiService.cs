using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.Services
{
    public interface IPizzaApiService
    {
        Task<Response<List<ShopItem>>> ListShops();

        Task<Response<List<PizzaItem>>> ListPizzas(int shopId);

        Task<Response<ImageSource>> ImagePizza(int shopId, int pizzaId);
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

        public async Task<Response<ImageSource>> ImagePizza(int shopId, int pizzaId)
        {
            string str1 = "{shopId}";
            string str2 = "{pizzaId}";
            return await _apiService.Get<Response<ImageSource>>(Urls.GET_IMAGE.Replace(str1, shopId.ToString()).Replace(str2, pizzaId.ToString()));
        }
        
    }
}