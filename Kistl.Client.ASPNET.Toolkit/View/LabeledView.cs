using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/LabeledView.ascx")]
    public abstract class LabeledView : ModelUserControl<ILabeledViewModel>
    {
        protected abstract Label lbCtrl { get; }
        protected abstract Control containerCtrl { get; }

        public override void SetModel(PresentableModel mdl)
        {
            base.SetModel(mdl);
            GuiApplicationContext.Current.Factory.CreateSpecificView(Model.Model, Model.RequestedKind, containerCtrl);
        }

        protected override void OnPreRender(EventArgs e)
        {
            lbCtrl.Text = Model.Label;
            lbCtrl.ToolTip = Model.ToolTip;
            base.OnPreRender(e);
        }
    }
}