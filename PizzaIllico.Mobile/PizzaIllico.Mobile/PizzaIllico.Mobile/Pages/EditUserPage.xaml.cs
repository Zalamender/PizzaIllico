using PizzaIllico.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPage
    {
        public EditUserPage()
        {
            BindingContext = new EditUserViewModel();
            InitializeComponent();
            
        }
    }
}