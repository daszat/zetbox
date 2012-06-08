using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;
using System.Web.UI;
using Kistl.API;
using Kistl.App.Extensions;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Kistl.Client.Presentables.ValueViewModels;

[assembly: WebResource("Kistl.Client.ASPNET.Toolkit.View.DataObjectReferenceView.js", "text/javascript")] 


namespace Kistl.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/DataObjectReferenceView.ascx")]
    public abstract class DataObjectReferenceView : ModelUserControl<ObjectReferenceViewModel>, IScriptControl
    {
        protected abstract Control containerCtrl { get; }
        protected abstract Label lbItemCtrl { get; }
        protected abstract HtmlControl btnNewCtrl { get; }
        protected abstract HtmlControl btnOpenCtrl { get; }

        private HiddenField _valueCtrl = null;

        public DataObjectReferenceView()
        {
            this.Init += new EventHandler(DataObjectReferenceView_Init);
        }

        void DataObjectReferenceView_Init(object sender, EventArgs e)
        {
            _valueCtrl = new HiddenField();
            _valueCtrl.ID = "hdValue";
            containerCtrl.Controls.Add(_valueCtrl);

        }

        public System.Collections.Generic.IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor desc = new ScriptControlDescriptor("Kistl.Client.ASPNET.ObjectReferencePropertyControl",
                containerCtrl.ClientID);
            desc.AddElementProperty("LnkOpen", btnOpenCtrl.ClientID);
            desc.AddElementProperty("ValueCtrl", _valueCtrl.ClientID);
            desc.AddProperty("Type", Model.ReferencedClass.GetDescribedInterfaceType().ToSerializableType());
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
                string moniker = _valueCtrl.Value;
                if (string.IsNullOrEmpty(moniker))
                {
                    return null;
                }
                else
                {
                    return (IDataObject)KistlContextManagerModule.KistlContext.Find(Model.ReferencedClass.GetDescribedInterfaceType(),
                        moniker.FromJSON(KistlContextManagerModule.KistlContext).ID);
                }
            }            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            lbItemCtrl.Text = Model.Value != null ? Model.Value.LongName : String.Empty;

            btnNewCtrl.Attributes.Add("onclick", string.Format("javascript: Kistl.JavascriptRenderer.showObject(Kistl.JavascriptRenderer.newObject({0}));",
                Model.ReferencedClass.GetDescribedInterfaceType().ToJSON()));

            _valueCtrl.Value = Model.Value != null ? Model.Value.ToJSON() : String.Empty;

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
