// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.Client.Presentables;
using Zetbox.Client.GUI;
using System.Web.UI;
using Zetbox.API;
using Zetbox.App.Extensions;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Zetbox.Client.Presentables.ValueViewModels;

[assembly: WebResource("Zetbox.Client.ASPNET.Toolkit.View.DataObjectReferenceView.js", "text/javascript")] 


namespace Zetbox.Client.ASPNET.Toolkit.View
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
            ScriptControlDescriptor desc = new ScriptControlDescriptor("Zetbox.Client.ASPNET.ObjectReferencePropertyControl",
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
                typeof(DataObjectReferenceView), "Zetbox.Client.ASPNET.Toolkit.View.DataObjectReferenceView.js"));
        }

        public Zetbox.API.IDataObject Value
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
                    return (IDataObject)ZetboxContextManagerModule.ZetboxContext.Find(Model.ReferencedClass.GetDescribedInterfaceType(),
                        moniker.FromJSON(ZetboxContextManagerModule.ZetboxContext).ID);
                }
            }            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            lbItemCtrl.Text = Model.Value != null ? Model.Value.LongName : String.Empty;

            btnNewCtrl.Attributes.Add("onclick", string.Format("javascript: Zetbox.JavascriptRenderer.showObject(Zetbox.JavascriptRenderer.newObject({0}));",
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
