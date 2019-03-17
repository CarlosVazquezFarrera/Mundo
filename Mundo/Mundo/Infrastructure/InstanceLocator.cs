

namespace Mundo.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ViewModels;
    public class InstanceLocator
    {
        #region Propiedades
        public MainViewModel Main
        {
            get;
            set;
        }
        #endregion

        #region contrsuctores
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion

    }
}
