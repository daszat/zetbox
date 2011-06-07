using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Kistl.API;
using Kistl.Client.ASPNET.Toolkit.Pages;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using System.Web.UI.HtmlControls;
using Kistl.Client.Presentables.ObjectEditor;

[assembly: WebResource("Kistl.Client.ASPNET.Toolkit.View.WorkspaceView.js", "text/javascript")]

namespace Kistl.Client.ASPNET.Toolkit.View
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
                KistlContextManagerModule.ViewModelFactory.ShowModel(item, false);
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

                    KistlContextManagerModule.ViewModelFactory.CreateDefaultView(mdl, container);
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

                        InterfaceType ifType = KistlContextManagerModule.IftFactory(Type.GetType(type));
                        IDataObject obj = (IDataObject)KistlContextManagerModule.KistlContext.Find(ifType, id);

                        var mdl = DataObjectViewModel.Fetch(KistlContextManagerModule.ViewModelFactory, KistlContextManagerModule.KistlContext, null, obj);
                        if (mdl == null) throw new InvalidOperationException(string.Format("Unable to create model for {0}({1})", type, id));
                        _Objects.Add(mdl);
                    }
                    else
                    {
                        // We are to early! So we need to parse the Request Variable directly
                        // If anyone knows a better way -> pls. get in touch with me.
                        _Objects = Request[hdObjectsControl.UniqueID].FromJSONArray(KistlContextManagerModule.KistlContext)
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
            var desc = new ScriptControlDescriptor("Kistl.Client.ASPNET.View.WorkspaceView", containerCtrl.ClientID);
            desc.AddProperty("ListObjects", _controlsAdded.Select(i => i.ClientID).ToList());
            desc.AddElementProperty("CurrentIndexCtrl", _currentIndexCtrl.ClientID);
            yield return desc;
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(WorkspaceView), "Kistl.Client.ASPNET.Toolkit.View.WorkspaceView.js"));
        }

        #endregion
    }
}
