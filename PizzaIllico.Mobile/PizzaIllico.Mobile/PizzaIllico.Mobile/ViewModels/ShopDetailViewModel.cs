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

namespace PizzaIllico.Mobile.ViewModels
{
    class ShopDetailViewModel : ViewModelBase
    {
		private string _shopName;
		public string ShopName
		{
			get => _shopName;
			set => SetProperty(ref _shopName, value);
		}
		private ObservableCollection<PizzaItem> _pizzas;

		public ObservableCollection<PizzaItem> Pizzas
		{
			get => _pizzas;
			set => SetProperty(ref _pizzas, value);
		}

		private int id;

		public ICommand SelectedCommand { get; }
		public ShopDetailViewModel(int id, string ShopName)
        {
			this.id = id;
			this.ShopName = ShopName;
			SelectedCommand = new Command<PizzaItem>(SelectedAction);
		}

		private void SelectedAction(PizzaItem obj)
		{
			INavigationService navigationService = DependencyService.Get<INavigationService>();
			navigationService.PushAsync(new Pages.PizzaDetailPage(obj, id));
		}
		public override async Task OnResume()
		{
			await base.OnResume();

			IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

			Response<List<PizzaItem>> response = await service.ListPizzas(id);
			Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
			if (response.IsSuccess)
			{
				Console.WriteLine($"Appel HTTP : {response.Data.Count}");
				Pizzas = new ObservableCollection<PizzaItem>(response.Data);
            }
		}
	}
}
