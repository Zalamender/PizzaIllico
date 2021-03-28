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
using System.ComponentModel;
using PizzaIllico.Mobile.Dtos.Accounts;

namespace PizzaIllico.Mobile.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SelectedCommand { get; }

        private string _username;

        public string Username
        {
            get => _username;
            set { _username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private CreateUserRequest _user;
        public CreateUserRequest User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


        public LoginViewModel()
        {
          
        }
        
       public Command LoginCommand
       {
           get
           {
               return new Command(Login);
           }
       }
      
       private void Login()
       {
           if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) {
               App.Current.MainPage.DisplayAlert("Error", "Please enter your username and password.", "OK");
               Console.WriteLine("TestVIDE");
           }
           else 
           {
                connexion();
           }
       }

        /**private async void LoginCall()
        {

        }
         **/

        public async Task connexion()
       {
 
           IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
           Response<List<ShopItem>> response = await service.Register();

           Console.WriteLine($"Appel HTTP : {response.IsSuccess}");

           if (response.IsSuccess)
           {
               Console.WriteLine($"Appel HTTP : {response.Data.Count}");
              
               }
           }
       }
      
    
}
