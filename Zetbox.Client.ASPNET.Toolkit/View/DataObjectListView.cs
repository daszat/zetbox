using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using Zetbox.API;
using Zetbox.App.Extensions;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;

[assembly: WebResource("Zetbox.Client.ASPNET.Toolkit.View.DataObjectListView.js", "text/javascript")] 

namespace Zetbox.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/DataObjectListView.ascx")]
    public abstract class DataObjectListView : ModelUserControl<ObjectListViewModel>, IScriptControl
    {
        protected abstract HiddenField HdItemsControl { get; }
        protected abstract AjaxDataControls.DataList LstItemsControl { get; }
        protected abstract Control ContainerControl { get; }
        protected abstract Control LnkAddControl { get; }
        protected abstract Control LnkNewControl { get; }

        public DataObjectListView()
        {
            this.Load += new EventHandler(DataObjectListView_Load);
        }

        void DataObjectListView_Load(object sender, EventArgs e)
        {
            // an empty array is posted as []
            // if the value is empty -> the Object is been displayed the first time
            if (!string.IsNullOrEmpty(HdItemsControl.Value))
            {
                var postedData = HdItemsControl.Value.FromJSONArray(ZetboxContextManagerModule.ZetboxContext).ToList();

                var added = postedData.Except(Model.Value).ToList();
                var deleted = Model.Value.Except(postedData).ToList();

                deleted.ForEach(d => Model.RemoveItem(d));
                added.ForEach(a => Model.AddItem(a));
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            HdItemsControl.Value = Model.Value.ToJSONArray();

            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager == null)
            {
                throw new InvalidOperationException(
                  "ScriptManager required on the page.");
            }

            scriptManager.RegisterScriptControl(this);

            Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), ContainerControl.ClientID,
                string.Format("$find('{0}').onSubmit();", ContainerControl.ClientID));
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            if (!DesignMode)
            {
                ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
            }
        }

        public System.Collections.Generic.IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor desc = new ScriptControlDescriptor("Zetbox.Client.ASPNET.ObjectListControl",
                ContainerControl.ClientID);
            desc.AddComponentProperty("List", LstItemsControl.ClientID);
            desc.AddElementProperty("Items", HdItemsControl.ClientID);
            desc.AddElementProperty("LnkAdd", LnkAddControl.ClientID);
            desc.AddElementProperty("LnkNew", LnkNewControl.ClientID);
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
                typeof(DataObjectListView), "Zetbox.Client.ASPNET.Toolkit.View.DataObjectListView.js"));
        }        
    }
}
