using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;
using System.Web.UI.WebControls;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/EnumSelectionView.ascx")]
    public abstract class EnumSelectionView: ModelUserControl<EnumerationPropertyModel>
    {
        protected abstract ListControl listCtrl { get; }

        public EnumSelectionView()
        {
            this.Init += new EventHandler(EnumSelectionView_Init);
        }

        void EnumSelectionView_Init(object sender, EventArgs e)
        {
            listCtrl.DataTextField = "Value";
            listCtrl.DataValueField = "Key";
            listCtrl.DataSource = Model.PossibleValues;
            listCtrl.DataBind();
            listCtrl.Items.Insert(0, new ListItem("", ""));
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            listCtrl.SelectedValue = Model.Value != null ? Model.Value.ToString() : "";
        }
    }
}
