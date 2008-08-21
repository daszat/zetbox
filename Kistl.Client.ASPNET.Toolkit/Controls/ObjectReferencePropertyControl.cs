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

[assembly: WebResource("Kistl.Client.ASPNET.Toolkit.Controls.ObjectReferencePropertyControl.js", "text/javascript")] 


namespace Kistl.Client.ASPNET.Toolkit.Controls
{
    public abstract class ObjectReferencePropertyControl : ValueControl<IDataObject>, IReferenceControl, IScriptControl
    {
        protected abstract Control ContainerControl { get; }
        protected abstract DropDownList cbListControl { get; }
        protected abstract HtmlControl btnNewControl { get; }
        protected abstract HtmlControl btnOpenControl { get; }

        public ObjectReferencePropertyControl()
        {
            this.Init += new EventHandler(ObjectReferencePropertyControl_Init);
        }

        void ObjectReferencePropertyControl_Init(object sender, EventArgs e)
        {
            cbListControl.SelectedIndexChanged += new EventHandler(cbList_OnSelectedIndexChanged);
        }

        protected void cbList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            NotifyUserInput();
        }

        public System.Collections.Generic.IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor desc = new ScriptControlDescriptor("Kistl.Client.ASPNET.ObjectReferencePropertyControl",
                ContainerControl.ClientID);
            desc.AddElementProperty("List", cbListControl.ClientID);
            desc.AddElementProperty("LnkOpen", btnOpenControl.ClientID);
            desc.AddProperty("Type", new SerializableType(ObjectType));
            yield return desc;
        }

        public System.Collections.Generic.IEnumerable<ScriptReference> GetScriptReferences()
        {
            // typeof(thisclass) is important!
            // This is a UserControl. ASP.NET will derive from this class.
            // this.GetType() wont return a Type, where Assembly is set to this Assembly
            // -> use typeof(thisclass) instead
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(ObjectListControl), "Kistl.Client.ASPNET.Toolkit.Controls.ObjectReferencePropertyControl.js"));
        }

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
                cbListControl.DataValueField = "Moniker";
                cbListControl.DataTextField = "Text";
                cbListControl.DataSource = value.Select(i => new { Moniker = i.ToJSON(), Text = i.ToString() });
                cbListControl.DataBind();

                cbListControl.Items.Insert(0, new ListItem("", ""));
            }
        }

        #endregion

        /// <summary>
        /// TODO: Entweder so mit dem Moniker oder wieder mit der ID. Dann muss ich aber clientseitig einen neuen Moniker aufbauen (für ShowObject).
        /// </summary>
        public override Kistl.API.IDataObject Value
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
                    return ((IBasicControl)this).Context.Find(ObjectType, moniker.FromJSON<IDataObject>(((IBasicControl)this).Context).ID);
                }
            }
            set
            {
                if (value != null)
                {
                    cbListControl.SelectedValue = value.ToJSON();
                }
                else
                {
                    cbListControl.SelectedValue = "";
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            btnNewControl.Attributes.Add("onclick", string.Format("javascript: Kistl.JavascriptRenderer.showObject(Kistl.JavascriptRenderer.newObject({0}));", 
                ObjectType.ToJSON()));
            
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