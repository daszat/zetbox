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

public partial class Controls_ObjectReferencePropertyControl : System.Web.UI.UserControl, IObjectReferenceControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cbList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.UserInput != null)
        {
            this.UserInput(this, EventArgs.Empty);
        }
    }

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

    #region IReferenceControl<IDataObject> Members

    public Type ObjectType
    {
        get;
        set;
    }

    public System.Collections.Generic.IList<Kistl.API.IDataObject> ItemsSource
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            cbList.DataValueField = "ID";
            cbList.DataTextField = "Text";
            cbList.DataSource = value.Select(i => new { ID = i.ID, Text = i.ToString() });
            cbList.DataBind();
        }
    }

    #endregion

    #region IValueControl<IDataObject> Members

    public Kistl.API.IDataObject Value
    {
        get
        {
            return new ObjectMoniker(Convert.ToInt32(cbList.SelectedValue), ObjectType);
        }
        set
        {
            if (value != null)
            {
                cbList.SelectedValue = value.ID.ToString();
            }
        }
    }

    public bool IsValidValue
    {
        get;
        set;
    }

    public event EventHandler UserInput;

    #endregion
}
