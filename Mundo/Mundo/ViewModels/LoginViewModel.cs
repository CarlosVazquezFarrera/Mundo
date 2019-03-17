namespace Mundo.ViewModels
{
    using System.Windows.Input;
    public class LoginViewModel
    {
        #region Propiedades
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRunning { get; set; }
        public bool IsRemember { get; set; }
        #endregion

        #region constructores
        public LoginViewModel()
        {
            this.IsRemember = true;
        }
        #endregion

        #region Comandos
        public ICommand LoginCommand { get; set; }
        #endregion
    }
}
