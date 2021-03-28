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
using PizzaIllico.Mobile.Dtos.Authentications;
using PizzaIllico.Mobile.Dtos.Authentications.Credentials;
using PizzaIllico.Mobile.Pages;

namespace PizzaIllico.Mobile.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        public ICommand SelectedCommand { get; }

        private string _login;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private LoginWithCredentialsRequest _userlog;
        public LoginWithCredentialsRequest UserLog
        {
            get => _userlog;
            set => SetProperty(ref _userlog, value);
        }


        public LoginViewModel()
        {

        }

        public Command LoginCommand
        {
            get
            {
                return new Command(loginUser);
            }
        }

        private async void loginUser()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                App.Current.MainPage.DisplayAlert("Erreur", "Un des champs obligatoire est vide.", "OK");
                Console.WriteLine("TestVIDE");
            }
            else
            {
                LoginWithCredentialsRequest userLog = new LoginWithCredentialsRequest
                {
                    ClientId = "MOBILE",
                    ClientSecret = "UNIV",
                    Login = this.Login,
                    Password = this.Password
                };

                IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
                Response<LoginResponse> response = await service.Login(userLog);

                Console.WriteLine($"Appel HTTP : {response.IsSuccess}");

                if (response.IsSuccess)
                {
                    Console.WriteLine("CONNEXION REUSSIE");
                    //  Console.WriteLine($"Appel HTTP : {response.Data.Count}");
                    User user = User.getInstance();
                    user.configure(this.Login, this.Password, response.Data.RefreshToken, response.Data.AccessToken);
                    Application.Current.MainPage = new MainPage();
                    Console.WriteLine("LOLPREMIER" + User.getInstance().getAccessToken());
                    Console.WriteLine("LOLTESTPREMIER" + User.getInstance().getUsername());
                }
                else
                {
                    Console.WriteLine("CONNEXION ECHEC");
                    App.Current.MainPage.DisplayAlert("Erreur", "Votre identifiant et/ou votre mot de passe est incorrect. ", "OK");
                }
            }

        }
    }
}



