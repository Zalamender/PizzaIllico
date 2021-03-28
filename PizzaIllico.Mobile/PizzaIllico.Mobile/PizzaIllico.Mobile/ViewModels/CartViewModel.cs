﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Storm.Mvvm;
using PizzaIllico.Mobile.Dtos.Pizzas;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;

namespace PizzaIllico.Mobile.ViewModels
{
    class CartViewModel : ViewModelBase
    {
        private Cart cart;


        private ObservableCollection<PizzaItem> _pizzas;
        public ObservableCollection<PizzaItem> Pizzas
        {
            get => _pizzas;
            set
            {
                _pizzas = value;
            }
        }

        private double _totalPrice;
        public double TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        public ICommand RemovePizzaCommand { get; }
        public ICommand EmptyListCommand { get; }
        public ICommand OrderCommand { get; }

        public CartViewModel()
        {
            cart = Cart.getInstance();
            actuateList();
            RemovePizzaCommand = new Command<PizzaItem>(RemovePizzaAction);
            EmptyListCommand = new Command(EmptyListAction);
            OrderCommand = new Command(OrderAction);
        }

        private void OrderAction()
        {
            if (Pizzas.Count != 0)
                OrderConfirmation();
        }

        private async void OrderConfirmation()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Order now ?", "Yes", "No");
            if (answer)
            {
                cart.Order();
                actuateList();
            }
        }

        private void EmptyListAction()
        {
            cart.EmptyList();
            actuateList();
        }

        private void actuateList()
        {
            Pizzas = cart.getPizzaList();
            double price = 0;
            foreach(PizzaItem pizza in Pizzas)
            {
                price += pizza.Price;
            }
            TotalPrice = price;
            
        }

        public void RemovePizzaAction(PizzaItem obj)
        {
            RemovalConfirmation(obj);
        }

        private async void RemovalConfirmation(PizzaItem obj)
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Are you sure you want to remove this item from your cart.", "Yes", "No");
            if (answer)
            {
                cart.RemovePizza(obj);
                actuateList();
            }
        }
        public override Task OnResume()
        {
            actuateList();
            return base.OnResume();
        }
    }
}