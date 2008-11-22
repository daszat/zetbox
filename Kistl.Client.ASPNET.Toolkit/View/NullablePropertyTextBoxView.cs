using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public class NullablePropertyTextBoxView : System.Web.UI.UserControl, IView
    {
        protected PresentableModel Model { get; private set; }

        public void SetModel(PresentableModel mdl)
        {
            Model = (PresentableModel)mdl;
        }
    }
}
