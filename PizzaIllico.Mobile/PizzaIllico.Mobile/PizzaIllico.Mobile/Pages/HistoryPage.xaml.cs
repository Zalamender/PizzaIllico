using PizzaIllico.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage
    {
        public HistoryPage()
        {
            BindingContext = new HistoryViewModel();
            InitializeComponent();
        }
    }
}