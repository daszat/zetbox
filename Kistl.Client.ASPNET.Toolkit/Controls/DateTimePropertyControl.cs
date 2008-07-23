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
    public abstract class DateTimePropertyControl : ValueControl<DateTime?>
    {
        protected abstract TextBox txtDateTimeControl { get; }

        public DateTimePropertyControl()
        {
            this.Init += new EventHandler(DateTimePropertyControl_Init);
        }

        void DateTimePropertyControl_Init(object sender, EventArgs e)
        {
            txtDateTimeControl.TextChanged += new EventHandler(txtDateTime_OnTextChanged);
        }

        protected void txtDateTime_OnTextChanged(object sender, EventArgs e)
        {
            NotifyUserInput();
        }

        public override DateTime? Value
        {
            get
            {
                return string.IsNullOrEmpty(txtDateTimeControl.Text) ? null : (DateTime?)Convert.ToDateTime(txtDateTimeControl.Text);
            }
            set
            {
                txtDateTimeControl.Text = value.ToString();
            }
        }
    }
}