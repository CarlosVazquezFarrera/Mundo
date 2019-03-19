
namespace Mundo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class PaisItemViewModel : Land
    {
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }

        private async void SelectLand()
        {
            MainViewModel.GetInstance().Pais = new PaisViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new PaisTabbedPage()); 
        }
    }
}
