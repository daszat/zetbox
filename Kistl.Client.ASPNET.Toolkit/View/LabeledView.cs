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
    public abstract class LabeledView : System.Web.UI.UserControl, IView
    {
        protected ILabeledViewModel Model { get; private set; }
        protected abstract Label lbCtrl { get; }
        protected abstract Control containerCtrl { get; }

        private bool _initialized = false;

        public LabeledView()
        {
            this.Init += new EventHandler(LabeledView_Init);
        }

        public void SetModel(PresentableModel mdl)
        {
            Model = (ILabeledViewModel)mdl;
            BindTo();
        }

        void LabeledView_Init(object sender, EventArgs e)
        {
            BindTo();
        }

        private void BindTo()
        {
            if (Model == null || _initialized) return;

            _initialized = true;
            
            lbCtrl.Text = Model.Label;
            lbCtrl.ToolTip = Model.ToolTip;

            GuiApplicationContext.Current.Factory.CreateSpecificView(Model.Model, Model.RequestedKind, containerCtrl);
        }
    }
}