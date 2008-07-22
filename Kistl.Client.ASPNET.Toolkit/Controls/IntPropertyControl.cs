using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Kistl.GUI;
using Kistl.API;

namespace Kistl.Client.ASPNET.Toolkit.Controls
{
    public abstract class IntPropertyControl : BasicPropertyControl<int?>
    {
        protected abstract TextBox txtIntControl { get; }

        public IntPropertyControl()
        {
            this.Init += new EventHandler(IntPropertyControl_Init);
        }

        void IntPropertyControl_Init(object sender, EventArgs e)
        {
            txtIntControl.TextChanged += new EventHandler(txtInt_OnTextChanged);
        }

        protected void txtInt_OnTextChanged(object sender, EventArgs e)
        {
            NotifyUserInput();
        }

        public override int? Value
        {
            get
            {
                return string.IsNullOrEmpty(txtIntControl.Text) ? null : (int?)Convert.ToInt32(txtIntControl.Text);
            }
            set
            {
                txtIntControl.Text = value.ToString();
            }
        }
    }
}