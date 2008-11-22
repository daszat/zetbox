using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public abstract class WorkspaceView : System.Web.UI.UserControl, IView
    {
        protected WorkspaceModel Model {get; private set;}

        public void SetModel(PresentableModel mdl)
        {
            Model = (WorkspaceModel)mdl;
        }
    }
}
