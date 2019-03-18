namespace Mundo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {

        #region Atributos

        private string password;
        private bool isrunning;
        private bool isEnabled;
        #endregion

        #region Propiedades
        public string Email { get; set; }
        public bool IsRemember { get; set; }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning
        {
            get { return this.isrunning; }
            set { SetValue(ref this.isrunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region constructores
        public LoginViewModel()
        {
            this.IsRemember = true;
            this.IsEnabled = true;
        }
        #endregion

        #region Comandos
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }



        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingresa un Email",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingresa una contraseña",
                    "Aceptar");
                this.Password = string.Empty;
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "carlos@unach.com" || this.Password != "12345")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email o contraseña incorrecto",
                    "Aceptar");
                this.Password = string.Empty;
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                "Sí jala",
                "Aceptar");
            return;
        }
        #endregion
    }
}
