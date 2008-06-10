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

public partial class Controls_DateTimePropertyControl : System.Web.UI.UserControl, IValueControl<DateTime?>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void txtDateTime_OnTextChanged(object sender, EventArgs e)
    {
        if (this.UserInput != null)
        {
            this.UserInput(this, EventArgs.Empty);
        }
    }

    #region IValueControl<string> Members

    public DateTime? Value
    {
        get
        {
            return string.IsNullOrEmpty(txtDateTime.Text) ? null : (DateTime?)Convert.ToDateTime(txtDateTime.Text);
        }
        set
        {
            txtDateTime.Text = value.ToString();
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
