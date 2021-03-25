using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ImageSource _pizzaImage;

        public ImageSource PizzaImage
        {
            get => _pizzaImage;
            set => SetProperty(ref _pizzaImage, value);
        }


        private int shopId;


        public PizzaDetailViewModel(PizzaItem pizza, int shopId)
        {
            this.Pizza = pizza;
            this.shopId = shopId;
        }

        public override async Task OnResume()
        {
            await base.OnResume();

            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<ImageSource> response = await service.ImagePizza((int)this.Pizza.Id,shopId);
            Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
            if (response.IsSuccess)
            {
                Console.WriteLine($"Appel HTTP : {response.Data}");
                PizzaImage = (response.Data);
            }
        }
    }
}
