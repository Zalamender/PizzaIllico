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

namespace PizzaIllico.Mobile.ViewModels
{
    class RegisterViewModel : ViewModelBase
    {
        public ICommand SelectedCommand { get; }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _firstname;

        public string FirstName
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastname;

        public string LastName
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged("LastName");
            }
        }

        private string _phonenumber;

        public string PhoneNumber
        {
            get => _phonenumber;
            set
            {
                _phonenumber = value;
                OnPropertyChanged("PhoneNumber");
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

        private CreateUserRequest _user;
        public CreateUserRequest User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


        public RegisterViewModel()
        {

        }

        public Command LoginCommand
        {
            get
            {
                return new Command(createUser);
            }
        }

        private async void createUser()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                App.Current.MainPage.DisplayAlert("Error", "Please enter your username and password.", "OK");
                Console.WriteLine("TestVIDE");
            }
            else
            {
                CreateUserRequest user = new CreateUserRequest
                {
                    ClientId = "MOBILE",
                    ClientSecret = "UNIV",
                    Email = _email,
                    FirstName=this.FirstName,
                    LastName=this.LastName,
                    PhoneNumber=this.PhoneNumber,
                    Password=this.Password
                };
                IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
                Response<LoginResponse> response = await service.Register(user);

                Console.WriteLine($"Appel HTTP : {response.IsSuccess}");

                if (response.IsSuccess)
                {
                    //  Console.WriteLine($"Appel HTTP : {response.Data.Count}");

                }
            }

        }
    }
}
      
    

