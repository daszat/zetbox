using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;
using System.Web.UI.WebControls;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public abstract class NullablePropertyTextBoxView : System.Web.UI.UserControl, IView
    {
        protected IValueModel<String> Model { get; private set; }
        protected abstract Label lbCtrl { get; }
        protected abstract TextBox txtCtrl { get; }

        public void SetModel(PresentableModel mdl)
        {
            Model = (IValueModel<String>)mdl;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            lbCtrl.Text = Model.Label;
            txtCtrl.Text = Model.Value;
        }
    }
}
