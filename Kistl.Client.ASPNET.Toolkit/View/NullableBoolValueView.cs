using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;
using System.Web.UI.WebControls;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/NullableBoolValueView.ascx")]
    public abstract class NullableBoolValueView : System.Web.UI.UserControl, IView
    {
        protected NullableValuePropertyModel<bool> Model { get; private set; }
        protected abstract RadioButton rbTrueCtrl { get; }
        protected abstract RadioButton rbFalseCtrl { get; }
        protected abstract RadioButton rbNullCtrl { get; }

        public NullableBoolValueView()
        {
        }

        public void SetModel(PresentableModel mdl)
        {
            Model = (NullableValuePropertyModel<bool>)mdl;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            rbNullCtrl.Visible = Model.AllowNullInput;
            rbTrueCtrl.Checked = false;
            rbFalseCtrl.Checked = false;
            rbNullCtrl.Checked = false;

            rbTrueCtrl.Checked = Model.Value == true;
            rbFalseCtrl.Checked = Model.Value == false;
            rbNullCtrl.Checked = Model.Value == null;
        }
    }
}
