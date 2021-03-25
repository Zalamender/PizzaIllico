using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PizzaIllico.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using PizzaIllico.Mobile.Dtos.Pizzas;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaDetailPage
    {
        public PizzaDetailPage(PizzaItem pizza, int shopId)
        {
            BindingContext = new PizzaDetailViewModel(pizza,shopId);
            InitializeComponent();
        }
    }
}