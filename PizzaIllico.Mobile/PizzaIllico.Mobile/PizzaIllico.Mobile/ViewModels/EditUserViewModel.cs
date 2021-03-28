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

namespace PizzaIllico.Mobile.ViewModels
{
    class EditUserViewModel : ViewModelBase
    {
        public ICommand SelectedCommand { get; }

        private string _oldpassword;

        public string OldPassword
        {
            get => _oldpassword;
            set
            {
                _oldpassword = value;
                OnPropertyChanged("OldPassword");
            }
        }

        private string _newpassword;

        public string NewPassword
        {
            get => _newpassword;
            set
            {
                _newpassword = value;
                OnPropertyChanged("NewPassword");
            }
        }

        private SetPasswordRequest _passwordRequest;
        public SetPasswordRequest PasswordRequest
        {
            get => _passwordRequest;
            set => SetProperty(ref _passwordRequest, value);
        }


        public EditUserViewModel()
        {

        }

        public Command LoginCommand
        {
            get
            {
                return new Command(editUser);
            }
        }

        private async void editUser()
        {
            Console.WriteLine("LOL"+User.getInstance().getAccessToken());
            Console.WriteLine("LOLTEST"+User.getInstance().getUsername());
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword))
            {
                App.Current.MainPage.DisplayAlert("Erreur", "Un des champs obligatoire est vide.", "OK");
                Console.WriteLine("TestVIDE");
            }
            else
            {
                if (OldPassword.CompareTo(User.getInstance().getPassword()) == 0)
                {
                    SetPasswordRequest passwordRequest = new SetPasswordRequest
                    {
                        OldPassword = this.OldPassword,
                        NewPassword = this.NewPassword

                    };

                    IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
                    Response response = await service.SetPassword(passwordRequest);

                    Console.WriteLine($"Appel HTTP : {response.IsSuccess}");

                    if (response.IsSuccess)
                    {
                        Console.WriteLine("CONNEXION REUSSIE");
                        //  Console.WriteLine($"Appel HTTP : {response.Data.Count}");
                        User user = User.getInstance();
                        user.setPassword(NewPassword);

                    }
                    else
                    {
                        Console.WriteLine("CONNEXION ECHEC");
                        App.Current.MainPage.DisplayAlert("Erreur", "Token invalide", "OK");
                    }
                }
                else
                {
                    Console.WriteLine("MOT D EPASSE ANCIEN DIFFERENT");
                }
            }

        }
    }
}



