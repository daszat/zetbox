using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public class DataObjectListView : System.Web.UI.UserControl, IView
    {
        protected ObjectListModel Model { get; private set; }

        public void SetModel(PresentableModel mdl)
        {
            Model = (ObjectListModel)mdl;
        }
    }
}
