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
    public abstract class DoublePropertyControl : BasicPropertyControl<double?>
    {
        protected abstract TextBox txtDoubleControl { get; }

        public DoublePropertyControl()
        {
            this.Init += new EventHandler(DoublePropertyControl_Init);
        }

        void DoublePropertyControl_Init(object sender, EventArgs e)
        {
            txtDoubleControl.TextChanged += new EventHandler(txtDouble_OnTextChanged);
        }

        protected void txtDouble_OnTextChanged(object sender, EventArgs e)
        {
            NotifyUserInput();
        }

        public override double? Value
        {
            get
            {
                return string.IsNullOrEmpty(txtDoubleControl.Text) ? null : (double?)Convert.ToDouble(txtDoubleControl.Text);
            }
            set
            {
                txtDoubleControl.Text = value.ToString();
            }
        }
    }
}