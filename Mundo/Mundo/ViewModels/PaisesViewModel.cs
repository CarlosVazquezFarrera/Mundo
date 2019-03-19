namespace Mundo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class PaisesViewModel : BaseViewModel
    {

        #region Servicios
        private ApiService apiService;
        #endregion

        #region Atributos
        private ObservableCollection<PaisItemViewModel> paises;
        private bool isRefreshing;
        private string filter;
        private List<Land> paisesLista;
        #endregion

        #region Propiedades
        public ObservableCollection<PaisItemViewModel> Paises
        {
            get { return this.paises; }
            set { SetValue(ref this.paises, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
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
            this.IsRefreshing = true;
            var con = await this.apiService.CheckConnection();

            if (!con.IsSuccess)
            {
                this.IsRefreshing = false;
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
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                                                                "Error en success",
                                                                response.Message,
                                                                 "Aceptar");
                return;

            }

            this.paisesLista = (List<Land>)response.Result;
            this.Paises = new ObservableCollection<PaisItemViewModel>(
                this.ToLandItemViewModel());
            this.IsRefreshing = false;
        }

        #region Metodos 
        private IEnumerable<PaisItemViewModel> ToLandItemViewModel()
        {
            return this.paisesLista.Select(l => new PaisItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });

        } 
        #endregion


        #endregion

        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
               return new RelayCommand(CargarPaises);
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }
        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Paises = new ObservableCollection<PaisItemViewModel>(
                this.ToLandItemViewModel());
            }
            else
            {
                this.Paises = new ObservableCollection<PaisItemViewModel>(
                    this.ToLandItemViewModel().Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}
