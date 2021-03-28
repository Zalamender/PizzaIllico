using PizzaIllico.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage
    {
        public MapPage()
        {
            BindingContext = new MapViewModel();
            InitializeComponent();
        }
    }
}