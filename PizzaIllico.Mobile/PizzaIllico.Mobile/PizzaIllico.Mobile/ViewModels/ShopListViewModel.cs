using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class ShopListViewModel : ViewModelBase
    {
	    private ObservableCollection<ShopItem> _shops;

	    public ObservableCollection<ShopItem> Shops
	    {
		    get => _shops;
		    set => SetProperty(ref _shops, value);
	    }

		public ICommand SelectedCommand { get; }

	    public ShopListViewModel()
	    {
		    SelectedCommand = new Command<ShopItem>(SelectedAction);
	    }

	    private void SelectedAction(ShopItem obj)
	    {
			Console.WriteLine("test");
			INavigationService navigationService = DependencyService.Get<INavigationService>();
			navigationService.PushAsync(new Pages.ShopDetailPage((int)obj.Id,obj.Name));
		}

	    public override async Task OnResume()
        {
	        await base.OnResume();

	        IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

	        Response<List<ShopItem>> response = await service.ListShops();

			Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
	        if (response.IsSuccess)
	        {
		        Console.WriteLine($"Appel HTTP : {response.Data.Count}");
				Shops = new ObservableCollection<ShopItem>(response.Data.OrderBy(x => x.MinutesPerKilometer));
			}
        }
    }
}