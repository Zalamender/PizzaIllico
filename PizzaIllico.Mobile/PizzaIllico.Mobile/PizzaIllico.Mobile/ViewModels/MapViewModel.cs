using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PizzaIllico.Mobile.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
		private List<ShopItem> _shops;

		public List<ShopItem> Shops
		{
			get => _shops;
			set => SetProperty(ref _shops, value);
		}

		public ICommand SelectedCommand { get; }

		public MapViewModel()
		{
			//SelectedCommand = new Command<ShopItem>(SelectedAction);
			
				
				Position position = new Position(48.8666667, 2.3166666666666664);
				MapSpan mapSpan = new MapSpan(position, 10, 10);
			 Map = new Map(mapSpan)
			{
				IsShowingUser = true,
				MapType = MapType.Hybrid
			};
		
		}

		public Map Map { get; private set; }

		/**
		 * private void SelectedAction(ShopItem obj)
		{
			INavigationService navigationService = DependencyService.Get<INavigationService>();
			navigationService.PushAsync(new Pages.ShopDetailPage((int)obj.Id, obj.Name));
		}
		**/
		public override async Task OnResume()
		{
			await base.OnResume();
			IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
			Response<List<ShopItem>> response = await service.ListShops();

			Console.WriteLine($"Appel HTTP : {response.IsSuccess}");

			if (response.IsSuccess)
			{
				Console.WriteLine($"Appel HTTP : {response.Data.Count}");
				Shops = response.Data;
				foreach(ShopItem shop in Shops)
                {
					Pin pin = new()
					{
						Label = shop.Name,
						Address = shop.Address,
						Type = PinType.Place,
						Position = new Position(shop.Latitude, shop.Longitude)
					};
					Map.Pins.Add(pin);
				}
			}
		}
	}
}