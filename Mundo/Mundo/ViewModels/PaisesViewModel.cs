namespace Mundo.ViewModels
{
    using Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Services;
    using Xamarin.Forms;

    public class PaisesViewModel : BaseViewModel
    {

        #region Servicios
        private ApiService apiService;
        #endregion

        #region Atributos
        private ObservableCollection<Land> paises;
        #endregion

        #region Propiedades
        public ObservableCollection<Land> Paises
        {
            get { return this.paises; }
            set { SetValue(ref this.paises, value); }
        }
        #endregion



        #region Contructores
        public PaisesViewModel()
        {
            this.apiService = new ApiService();
            this.CargarPaises();
        }
        #endregion


        #region Metodos
        private async void CargarPaises()
        {
            var con = await this.apiService.CheckConnection();

            if (!con.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                                                                "Error",
                                                                con.Message,
                                                                 "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                                                                "Error en success",
                                                                response.Message,
                                                                 "Aceptar");
                return;

            }

            var lista = (List <Land>) response.Result;
            this.Paises = new ObservableCollection<Land>(lista);
        }
        #endregion
    }
}
