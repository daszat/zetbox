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

public partial class Controls_ObjectListControl : System.Web.UI.UserControl, IObjectListControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region IReferenceControl<IList<IDataObject>> Members

    public Kistl.API.ObjectType ObjectType
    {
        get;
        set;
    }

    public System.Collections.Generic.IList<Kistl.API.IDataObject> ItemsSource
    {
        get;
        set;
    }

    #endregion

    #region IValueControl<IList<IDataObject>> Members

    public System.Collections.Generic.IList<Kistl.API.IDataObject> Value
    {
        get
        {
            throw new NotSupportedException();
        }
        set
        {
            repItems.DataSource = value;
            repItems.DataBind();
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
