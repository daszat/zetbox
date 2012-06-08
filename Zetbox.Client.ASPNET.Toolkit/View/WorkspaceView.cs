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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Zetbox.API;
using Zetbox.Client.ASPNET.Toolkit.Pages;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using System.Web.UI.HtmlControls;
using Zetbox.Client.Presentables.ObjectEditor;

[assembly: WebResource("Zetbox.Client.ASPNET.Toolkit.View.WorkspaceView.js", "text/javascript")]

namespace Zetbox.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/WorkspaceView.ascx")]
    public abstract class WorkspaceView : ModelUserControl<WorkspaceViewModel>, IScriptControl
    {
        protected abstract Control containerCtrl { get; }
        protected abstract HiddenField hdObjectsControl { get; }
        protected abstract Repeater repObjectsCtrl { get; }
        protected abstract Control containerObjectsCtrl { get; }

        private List<Control> _controlsAdded = new List<Control>();
        private HiddenField _currentIndexCtrl = null;

        public WorkspaceView()
        {
            this.Init += new EventHandler(WorkspaceView_Init);
        }

        void WorkspaceView_Init(object sender, EventArgs e)
        {
            repObjectsCtrl.ItemDataBound += new RepeaterItemEventHandler(repObjectsCtrl_ItemDataBound);
            _currentIndexCtrl = new HiddenField();
            _currentIndexCtrl.ID = "hdCurrentIndex";
            containerCtrl.Controls.Add(_currentIndexCtrl);

            foreach (var item in Objects)
            {
                ZetboxContextManagerModule.ViewModelFactory.ShowModel(item, false);
            }

            if (!IsPostBack)
            {
                CurrentIndex = 0;
            }
        }

        public override void SetModel(ViewModel mdl)
        {
            base.SetModel(mdl);
            Model.Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Items_CollectionChanged);
        }

        void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (DataObjectViewModel mdl in e.NewItems)
                {
                    var container = new HtmlGenericControl("div");
                    container.ID = "mdlContainer" + Model.Items.IndexOf(mdl).ToString();
                    container.Attributes.Add("display", "node");
                    containerObjectsCtrl.Controls.Add(container);

                    ZetboxContextManagerModule.ViewModelFactory.CreateDefaultView(mdl, container);
                    _controlsAdded.Add(container);
                }
            }
        }

        #region Binding
        void repObjectsCtrl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.In(ListItemType.Item, ListItemType.AlternatingItem))
            {
                var data = (DataObjectViewModel)e.Item.DataItem;
                var litText = (Literal)e.Item.FindControl("litText");
                var container = (IAttributeAccessor)e.Item.FindControl("container");

                litText.Text = data.LongName;
                container.SetAttribute("onclick", string.Format("javascript: $find('{0}').SetObjectVisible({1});", containerCtrl.ClientID, e.Item.ItemIndex));
            }
        }
        #endregion

        #region State Management
        public int CurrentIndex
        {
            get
            {
                return Convert.ToInt32(_currentIndexCtrl.Value);
            }
            set
            {
                _currentIndexCtrl.Value = value.ToString();
            }
        }

        List<DataObjectViewModel> _Objects;
        public List<DataObjectViewModel> Objects
        {
            get
            {
                if (_Objects == null)
                {
                    if (!IsPostBack)
                    {
                        _Objects = new List<DataObjectViewModel>();
                        // Parse Request
                        var type = Request["type"];
                        var id = Convert.ToInt32(Request["id"]);

                        InterfaceType ifType = ZetboxContextManagerModule.IftFactory(Type.GetType(type));
                        IDataObject obj = (IDataObject)ZetboxContextManagerModule.ZetboxContext.Find(ifType, id);

                        var mdl = DataObjectViewModel.Fetch(ZetboxContextManagerModule.ViewModelFactory, ZetboxContextManagerModule.ZetboxContext, null, obj);
                        if (mdl == null) throw new InvalidOperationException(string.Format("Unable to create model for {0}({1})", type, id));
                        _Objects.Add(mdl);
                    }
                    else
                    {
                        // We are to early! So we need to parse the Request Variable directly
                        // If anyone knows a better way -> pls. get in touch with me.
                        _Objects = Request[hdObjectsControl.UniqueID].FromJSONArray(ZetboxContextManagerModule.ZetboxContext)
                                        .ToList();
                    }
                }

                return _Objects;
            }
        }
        #endregion

        #region Render
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            repObjectsCtrl.DataSource = Model.Items;
            repObjectsCtrl.DataBind();

            hdObjectsControl.Value = Model.Items.Cast<DataObjectViewModel>().ToJSONArray();

            CurrentIndex = Model.Items.IndexOf(Model.SelectedItem);

            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager == null) throw new InvalidOperationException("ScriptManager required on the page.");
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
        #endregion

        #region IScriptControl Members

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var desc = new ScriptControlDescriptor("Zetbox.Client.ASPNET.View.WorkspaceView", containerCtrl.ClientID);
            desc.AddProperty("ListObjects", _controlsAdded.Select(i => i.ClientID).ToList());
            desc.AddElementProperty("CurrentIndexCtrl", _currentIndexCtrl.ClientID);
            yield return desc;
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(WorkspaceView), "Zetbox.Client.ASPNET.Toolkit.View.WorkspaceView.js"));
        }

        #endregion
    }
}
