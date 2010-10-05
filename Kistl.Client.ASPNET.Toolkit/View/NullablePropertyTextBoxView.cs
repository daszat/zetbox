using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;
using System.Web.UI.WebControls;
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/NullablePropertyTextBoxView.ascx")]
    public abstract class NullablePropertyTextBoxView : ModelUserControl<IValueViewModel<String>>
    {
        protected abstract TextBox txtCtrl { get; }

        public NullablePropertyTextBoxView()
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            txtCtrl.Text = Model.Value;
        }
    }
}
