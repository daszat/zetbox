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
    public abstract class BoolPropertyControl : System.Web.UI.UserControl, IValueControl<bool?>
    {
        protected abstract CheckBox cbBoolControl { get; }

        public BoolPropertyControl()
        {
            this.Init += new EventHandler(BoolPropertyControl_Init);
        }

        void BoolPropertyControl_Init(object sender, EventArgs e)
        {
            cbBoolControl.CheckedChanged += new EventHandler(cbBool_OnCheckedChanged);
        }

        protected void cbBool_OnCheckedChanged(object sender, EventArgs e)
        {
            if (this.UserInput != null)
            {
                this.UserInput(this, EventArgs.Empty);
            }
        }

        #region IValueControl<string> Members

        IKistlContext IBasicControl.Context { get; set; }
        public bool? Value
        {
            get
            {
                return cbBoolControl.Checked;
            }
            set
            {
                cbBoolControl.Checked = value ?? false;
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