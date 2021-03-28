using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Services;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    /**
     *  Singleton représentant le panier de l'utilisateur.
     */
    public sealed class Cart
    {
        private static Lazy<Cart> cart = new Lazy<Cart>(() => new Cart());
        private ObservableCollection<PizzaItem> pizzaList = new ObservableCollection<PizzaItem>();
        private Dictionary<long, List<PizzaItem>> orderPreparator;

        private Cart()
        {
            pizzaList = new ObservableCollection<PizzaItem>();
            orderPreparator = new Dictionary<long, List<PizzaItem>>();
        }

        public static Cart getInstance()
        {
            return cart.Value;
        }

        public void AddPizza(PizzaItem pizza, long idShop)
        {
            pizzaList.Add(pizza);
            if (orderPreparator.ContainsKey(idShop))
            {
                orderPreparator[idShop].Add(pizza);
            }
            else
            {
                orderPreparator.Add(idShop, new List<PizzaItem>());
                orderPreparator[idShop].Add(pizza);
            }
        }

        public void RemovePizza(PizzaItem pizza)
        {
            pizzaList.Remove(pizza);
            foreach (KeyValuePair<long, List<PizzaItem>> pair in orderPreparator)
            {
                if (pair.Value.Contains(pizza))
                {
                    pair.Value.Remove(pizza);
                    break;
                }
            }
        }

        public void EmptyList()
        {
            pizzaList.Clear();
            foreach (KeyValuePair<long, List<PizzaItem>> pair in orderPreparator)
            {
                pair.Value.Clear();
            }
        }

        // Processes orders placed on every shops in the dictionary.
        public void Order()
        {
            List<long> ids;
            foreach (KeyValuePair<long, List<PizzaItem>> pair in orderPreparator)
            {
                ids = new List<long>();
                foreach (PizzaItem pizza in pair.Value)
                {
                    ids.Add(pizza.Id);
                }
                OrderRequest(pair.Key, ids);
            }
        }
        // Add order on api
        private async void OrderRequest(long idShop, List<long> ids)
        {
            CreateOrderRequest orderRequest = new()
            {
                PizzaIds = ids
            };
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
            Response<OrderItem> response = await service.Order(idShop, orderRequest); ;
            if (response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Commande", "Votre commande a été effectué !", "Ok !");
                EmptyList();
            }
        }

    public ObservableCollection<PizzaItem> getPizzaList()
    {
        return pizzaList;
    }
}
}
