using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    class PizzaDetailViewModel : ViewModelBase
    {
        private PizzaItem _pizza;

        public PizzaItem Pizza
        {
            get => _pizza;
            set => SetProperty(ref _pizza, value);
        }

        private string _pizzaImage;

        public string PizzaImage
        {
            get => _pizzaImage;
            set => SetProperty(ref _pizzaImage, value);
        }

        private int _ammount;

        public int Ammount
        {
            get => _ammount;
            set
            {
                _ammount = value;
                OnPropertyChanged("Ammount");
            }
        }


        private int shopId;

        private Cart cart;

        public ICommand IncrementCommand { get; }

        public ICommand DecrementCommand { get; }
        public ICommand AddCartCommand { get; }

        public PizzaDetailViewModel(PizzaItem pizza, int shopId)
        {
            this.Pizza = pizza;
            this.shopId = shopId;
            cart = Cart.getInstance();
            Ammount = 1;
            AddCartCommand = new Command(AddCartAction);
            IncrementCommand = new Command(IncrementAction);
            DecrementCommand = new Command(DecrementAction);
        }

        private void AddCartAction()
        {
            for(int i = 0; i < Ammount; i++)
            {
                cart.AddPizza(Pizza,shopId);
            }
            CartAdditionSuccess();
        }

        private async void CartAdditionSuccess()
        {
            await Application.Current.MainPage.DisplayAlert("Pizza added", "Your order has been added to the cart.", "OK");
        }

        private void IncrementAction()
        {
            Ammount++;
        }

        private void DecrementAction()
        {
            if(Ammount != 0)
            {
                Ammount--;
            }
        }

        public override async Task OnResume()
        {
            await base.OnResume();

            string str1 = "{shopId}";
            string str2 = "{pizzaId}";
            PizzaImage = "https://pizza.julienmialon.ovh/"+ Urls.GET_IMAGE.Replace(str1, shopId.ToString()).Replace(str2, Pizza.Id.ToString());
        }
    }
}
