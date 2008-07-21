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
    public abstract class DoublePropertyControl : System.Web.UI.UserControl, IValueControl<double?>
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
            if (this.UserInput != null)
            {
                this.UserInput(this, EventArgs.Empty);
            }
        }

        #region IValueControl<string> Members

        IKistlContext IBasicControl.Context { get; set; }
        public double? Value
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

        public bool IsValidValue
        {
            get;
            set;
        }

        public event EventHandler UserInput;

        #endregion

        #region IBasicControl Members

        public string ShortLabel
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public FieldSize Size
        {
            get;
            set;
        }

        #endregion
    }
}