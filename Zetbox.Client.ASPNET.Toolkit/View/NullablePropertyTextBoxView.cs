using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.Client.Presentables;
using Zetbox.Client.GUI;
using System.Web.UI.WebControls;
using Zetbox.Client.Presentables.ValueViewModels;

namespace Zetbox.Client.ASPNET.Toolkit.View
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
