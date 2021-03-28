﻿using PizzaIllico.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage
    {
        public RegisterPage()
        {
            BindingContext = new RegisterViewModel();
            InitializeComponent();
        }
    }
}