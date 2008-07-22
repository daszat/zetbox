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
    public abstract class StringPropertyControl : BasicPropertyControl<string>
    {
        protected abstract TextBox txtStringControl { get; }

        public StringPropertyControl()
        {
            this.Init += new EventHandler(StringPropertyControl_Init);
        }

        void StringPropertyControl_Init(object sender, EventArgs e)
        {
            txtStringControl.TextChanged += new EventHandler(txtString_OnTextChanged);
        }

        protected void txtString_OnTextChanged(object sender, EventArgs e)
        {
            NotifyUserInput();
        }

        public override string Value
        {
            get
            {
                return txtStringControl.Text;
            }
            set
            {
                txtStringControl.Text = value;
            }
        }
    }
}