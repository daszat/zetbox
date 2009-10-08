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

        public LabeledView()
        {
            this.Init += new EventHandler(LabeledView_Init);
        }

        void LabeledView_Init(object sender, EventArgs e)
        {
            InititalizeView();
        }

        public override void SetModel(PresentableModel mdl)
        {
            base.SetModel(mdl);
            InititalizeView();
        }

        private bool _initialized = false;
        private void InititalizeView()
        {
            if (!_initialized && Model != null)
            {
                GuiApplicationContext.Current.Factory.CreateSpecificView(Model.Model, Model.RequestedKind, containerCtrl);
                _initialized = true;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            lbCtrl.Text = Model.Label;
            lbCtrl.ToolTip = Model.ToolTip;
            base.OnPreRender(e);
        }
    }
}