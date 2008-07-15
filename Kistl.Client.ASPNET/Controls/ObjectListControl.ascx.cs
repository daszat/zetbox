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
using System.Collections.Generic;
using Kistl.API;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using Kistl.Client.ASPNET.Toolkit;

public partial class Controls_ObjectListControl : System.Web.UI.UserControl, IReferenceListControl
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Include_ObjectListControl", ResolveClientUrl("ObjectListControl.js"));

        if (IsPostBack)
        {
            var postedData = hdItems.Value.FromJSONArray(((IBasicControl)this).Context).ToList();

            var added = postedData.Except(_Value).ToList();
            var deleted = _Value.Except(postedData).ToList();

            deleted.ForEach(d => _Value.Remove(d));
            added.ForEach(a => _Value.Add(a));
        }
    }

    IKistlContext IBasicControl.Context { get; set; }

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

    ObservableCollection<IDataObject> _Value = new ObservableCollection<IDataObject>();

    ObservableCollection<IDataObject> IValueControl<ObservableCollection<IDataObject>>.Value
    {
        get
        {
            return _Value;
        }
        set
        {
            _Value = value;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        hdItems.Value = _Value.ToJSONArray();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Startup_ObjectListControl_" + this.ClientID,
            string.Format("Sys.Application.add_load(function() {{ objectListControl_DataBind('{0}', '{1}'); }}); \n",
                lstItems.ClientID, hdItems.ClientID), true);
        Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "OnSubmit_ObjectListControl_" + this.ClientID,
            string.Format("objectListControl_OnSubmit('{0}', '{1}');\n",
                lstItems.ClientID, hdItems.ClientID));

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Callback_ObjectListControl",
            "function objectListControl_CallServer(arg, context) {"
                + Page.ClientScript.GetCallbackEventReference(this, "arg", "return true;", "") + ";}", true);
    }
}
