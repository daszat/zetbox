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
    public abstract class DateTimePropertyControl : System.Web.UI.UserControl, IValueControl<DateTime?>
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
            if (this.UserInput != null)
            {
                this.UserInput(this, EventArgs.Empty);
            }
        }

        #region IValueControl<string> Members

        IKistlContext IBasicControl.Context { get; set; }

        public DateTime? Value
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