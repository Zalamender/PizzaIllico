using System;
using System.Windows.Input;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;
using PizzaIllico.Mobile.Dtos.Authentications;
using PizzaIllico.Mobile.Dtos.Authentications.Credentials;
using Storm.Mvvm.Services;

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
        public Command RegisterCommand
        {
            get
            {
                return new Command(registerUser);
            }
        }
        private async void registerUser()
        {

            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PushAsync(new Pages.RegisterPage());
        }

        private async void loginUser()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Erreur", "Un des champs obligatoire est vide.", "Ok !");
            }
            else
            {
                LoginWithCredentialsRequest userLog = new()
                {
                    ClientId = "MOBILE",
                    ClientSecret = "UNIV",
                    Login = this.Login,
                    Password = this.Password
                };

                IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
                Response<LoginResponse> response = await service.Login(userLog);

                if (response.IsSuccess)
                {
                    Console.WriteLine("CONNEXION REUSSIE");
                    User user = User.getInstance();
                    user.configure(response.Data.RefreshToken, response.Data.AccessToken);
                    INavigationService navigationService = DependencyService.Get<INavigationService>();
                    await navigationService.PushAsync(new Pages.MainPage());
                }
                else
                {
                    Console.WriteLine("CONNEXION ECHEC");
                    await App.Current.MainPage.DisplayAlert("Erreur", "Votre identifiant et/ou votre mot de passe est incorrect. ", "Ok !");
                }
            }
        }
    }
}



