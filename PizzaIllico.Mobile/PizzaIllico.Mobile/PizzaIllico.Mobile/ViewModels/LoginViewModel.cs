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

namespace PizzaIllico.Mobile.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                App.Current.MainPage.DisplayAlert("Error", "Please enter your username and password.", "OK");
            else 
            {
                
            }
        }

        /**private async void LoginCall()
        {

        }**/
    }
}
