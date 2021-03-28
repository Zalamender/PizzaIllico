using System;
using System.Windows.Input;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;
using PizzaIllico.Mobile.Dtos.Authentications.Credentials;
using PizzaIllico.Mobile.Dtos.Accounts;
using System.Threading.Tasks;

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

        public override async Task OnResume()
        {
            await base.OnResume();

            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<UserProfileResponse> response = await service.ProfileInfo();

            Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
            if (response.IsSuccess)
            {
                Console.WriteLine($"Appel HTTP : {response.Data}");
                Email = response.Data.Email;
                FirstName = response.Data.FirstName;
                LastName = response.Data.LastName;
                PhoneNumber = response.Data.PhoneNumber;
            }
        }

        public EditUserViewModel()
        {

        }

        public Command PasswordSetCommand
        {
            get
            {
                return new Command(EditPassword);
            }
        }
        
        public Command UserSetCommand
        {
            get
            {
                return new Command(EditUser);
            }
        }

        private async void EditPassword()
        {
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword))
                await App.Current.MainPage.DisplayAlert("Erreur", "Un des champs obligatoire est vide.", "Ok !");
            else
            {
                if (NewPassword.Length >= 8)
                {
                    SetPasswordRequest passwordRequest = new ()
                    {
                        OldPassword = this.OldPassword,
                        NewPassword = this.NewPassword
                    };

                    IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
                    Response response = await service.SetPassword(passwordRequest);

                    if (response.IsSuccess)
                    {
                        await App.Current.MainPage.DisplayAlert("Succès", "Votre mot de passe a été modifié.", "Ok !");
                    }
                    else
                    {
                        Console.WriteLine("Token invalide");
                        await App.Current.MainPage.DisplayAlert("Erreur", "Votre mot de passe est incorrect.", "Ok !");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Erreur", "Votre nouveau mot de passe doit contenir 8 caractères.", "Ok !");
                }
            }

        }

        private async void EditUser()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(PhoneNumber))
                await App.Current.MainPage.DisplayAlert("Erreur", "Un des champs obligatoire est vide.", "Ok !");
            else
            {
                SetUserProfileRequest userProfilRequest = new()
                    {
                        Email = this.Email,
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        PhoneNumber = this.PhoneNumber
                    };

                    IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
                    Response response = await service.SetProfile(userProfilRequest);
                 Console.WriteLine(response);
                    if (response.IsSuccess)
                    {
                        await App.Current.MainPage.DisplayAlert("Succès", "Votre profil a été modifié.", "Ok !");
                    }
                    else
                    {
                        Console.WriteLine("Token invalide");
                        await App.Current.MainPage.DisplayAlert("Erreur", "Votre connexion a expirée.", "Ok !");
                    }
            }

        }
    }
}