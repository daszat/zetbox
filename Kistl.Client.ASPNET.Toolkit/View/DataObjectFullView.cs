using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public class DataObjectFullView : System.Web.UI.UserControl, IView
    {
        protected DataObjectModel Model { get; private set; }

        public void SetModel(PresentableModel mdl)
        {
            Model = (DataObjectModel)mdl;
        }
    }
}
