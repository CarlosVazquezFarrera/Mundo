
namespace Mundo.ViewModels
{
    using Models;
    public class PaisViewModel
    {

        public Land  Pais { get; set; }

        #region Constructores

        public PaisViewModel(Land pais)
        {
            this.Pais = pais;
        } 
        #endregion
    }
}
