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

public partial class Controls_ObjectListControl : System.Web.UI.UserControl, IReferenceListControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public Type ObjectType
    {
        get;
        set;
    }

    public System.Collections.Generic.IList<Kistl.API.IDataObject> ItemsSource
    {
        get;
        set;
    }

    public bool IsValidValue
    {
        get;
        set;
    }

    public event EventHandler UserInput;

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

    public event EventHandler UserAddRequest;

    System.Collections.ObjectModel.ObservableCollection<Kistl.API.IDataObject> _Value;

    System.Collections.ObjectModel.ObservableCollection<Kistl.API.IDataObject> IValueControl<System.Collections.ObjectModel.ObservableCollection<Kistl.API.IDataObject>>.Value
    {
        get
        {
            return _Value;
        }
        set
        {
            _Value = value;
            repItems.DataSource = _Value;
            repItems.DataBind();
        }
    }
}
