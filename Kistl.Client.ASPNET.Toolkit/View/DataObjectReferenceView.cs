using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;
using System.Web.UI;
using Kistl.API;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

[assembly: WebResource("Kistl.Client.ASPNET.Toolkit.View.DataObjectReferenceView.js", "text/javascript")] 


namespace Kistl.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/DataObjectReferenceView.ascx")]
    public abstract class DataObjectReferenceView : System.Web.UI.UserControl, IView, IScriptControl
    {
        protected ObjectReferenceModel Model { get; private set; }

        protected abstract Control ContainerControl { get; }
        protected abstract DropDownList cbListControl { get; }
        protected abstract HtmlControl btnNewControl { get; }
        protected abstract HtmlControl btnOpenControl { get; }

        public DataObjectReferenceView()
        {
            this.Init += new EventHandler(DataObjectReferenceView_Init);
        }

        void DataObjectReferenceView_Init(object sender, EventArgs e)
        {
            cbListControl.SelectedIndexChanged += new EventHandler(cbList_OnSelectedIndexChanged);
        }

        protected void cbList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void SetModel(PresentableModel mdl)
        {
            Model = (ObjectReferenceModel)mdl;
        }

        public System.Collections.Generic.IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor desc = new ScriptControlDescriptor("Kistl.Client.ASPNET.ObjectReferencePropertyControl",
                ContainerControl.ClientID);
            desc.AddElementProperty("List", cbListControl.ClientID);
            desc.AddElementProperty("LnkOpen", btnOpenControl.ClientID);
            desc.AddProperty("Type", new SerializableType(new InterfaceType(typeof(IDataObject))));
            yield return desc;
        }

        public System.Collections.Generic.IEnumerable<ScriptReference> GetScriptReferences()
        {
            // typeof(thisclass) is important!
            // This is a UserControl. ASP.NET will derive from this class.
            // this.GetType() wont return a Type, where Assembly is set to this Assembly
            // -> use typeof(thisclass) instead
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(DataObjectReferenceView), "Kistl.Client.ASPNET.Toolkit.View.DataObjectReferenceView.js"));
        }

        public Kistl.API.IDataObject Value
        {
            get
            {
                string moniker = cbListControl.SelectedValue;
                if (string.IsNullOrEmpty(moniker))
                {
                    return null;
                }
                else
                {
                    return (IDataObject)KistlContextManagerModule.KistlContext.Find(new InterfaceType(typeof(IDataObject)),
                        moniker.FromJSON(KistlContextManagerModule.KistlContext).ID);
                }
            }            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            cbListControl.DataValueField = "Moniker";
            cbListControl.DataTextField = "Text";
            cbListControl.DataSource = Model.GetDomain().Select(i => new { Moniker = i.ToJSON(), Text = i.Name });
            cbListControl.DataBind();

            if (Model.AllowNullInput)
            {
                cbListControl.Items.Insert(0, new ListItem("", ""));
            }

            cbListControl.SelectedValue = Model.Value != null ? Model.Value.ToJSON() : "";

            btnNewControl.Attributes.Add("onclick", string.Format("javascript: Kistl.JavascriptRenderer.showObject(Kistl.JavascriptRenderer.newObject({0}));",
                new InterfaceType(typeof(IDataObject)).ToJSON()));

            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager == null)
            {
                throw new InvalidOperationException(
                  "ScriptManager required on the page.");
            }

            scriptManager.RegisterScriptControl(this);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            if (!DesignMode)
            {
                ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
            }
        }
    }
}
