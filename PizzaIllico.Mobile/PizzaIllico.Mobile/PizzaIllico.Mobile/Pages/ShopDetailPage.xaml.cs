using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PizzaIllico.Mobile.ViewModels;
using Storm.Mvvm.Forms;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopDetailPage
    {
        public ShopDetailPage(int id, string shopName)
        {
            BindingContext = new ShopDetailViewModel(id,shopName);
            InitializeComponent();
        }
    }
}