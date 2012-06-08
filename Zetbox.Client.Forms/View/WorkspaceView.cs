using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ZetboxBase;
using Zetbox.Client.Presentables.ObjectBrowser;

namespace Zetbox.Client.Forms.View
{
    public partial class WorkspaceView : Form, IFormsView
    {
        private WorkspaceViewModel _dataContextCache;
        public WorkspaceViewModel DataContext
        {
            get
            {
                return _dataContextCache;
            }
            internal set
            {
                if (_dataContextCache != null)
                {
                    throw new InvalidOperationException("DataContext may only be set once!");
                }

                _dataContextCache = value;
                _dataContextCache.Modules.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Modules_CollectionChanged);
                _dataContextCache.PropertyChanged += new PropertyChangedEventHandler(_dataContextCache_PropertyChanged);
                SyncModules();
            }
        }

        void _dataContextCache_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                if (_dataContextCache.SelectedItem == null)
                {
                    label1.Text = "Nothing selected";
                }
                else
                {
                    label1.Text = _dataContextCache.SelectedItem.ToString();
                    // TODO: dispose cleared controls
                    _viewPanel.Controls.Clear();
                    Renderer.ShowModel(DataContext.SelectedItem, _viewPanel);
                }
            }
        }

        void Modules_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SyncModules();
        }

        private void SyncModules()
        {
            System.Diagnostics.Debug.WriteLine("SyncModules");
            _moduleList.BeginUpdate();
            try
            {
                _moduleList.Items.Clear();
                foreach (var dom in _dataContextCache.Modules)
                {
                    var lvi = new ListViewItem(dom.Name) { Tag = dom };
                    dom.PropertyChanged += new PropertyChangedEventHandler(ModulePropertyChanged);
                    _moduleList.Items.Add(lvi);
                }
            }
            finally
            {
                _moduleList.EndUpdate();
            }
        }

        void ModulePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
                SyncModules();
        }
        void ObjectClassPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name" && _moduleList.SelectedItems.Count > 0)

                SyncObjectClasses((ModuleViewModel)_moduleList.SelectedItems[0].Tag);
        }
        void InstancePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name" && _instancesList.SelectedItems.Count > 0)
                SyncInstances((InstanceListViewModel)_instancesList.SelectedItems[0].Tag);
        }
        public WorkspaceView()
        {
            InitializeComponent();
        }

        private void _objectClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_objectClassList.SelectedItems.Count > 0)
            {
                SyncInstances((InstanceListViewModel)_objectClassList.SelectedItems[0].Tag);
            }
        }

        private void SyncObjectClasses(ModuleViewModel mdlMdl)
        {
            System.Diagnostics.Debug.WriteLine("SyncModules");
            _objectClassList.BeginUpdate();
            try
            {
                _objectClassList.Items.Clear();
                foreach (var dom in mdlMdl.ObjectClasses)
                {
                    var lvi = new ListViewItem(dom.Name) { Tag = dom };
                    dom.PropertyChanged += new PropertyChangedEventHandler(ObjectClassPropertyChanged);
                    _objectClassList.Items.Add(lvi);
                }
            }
            finally
            {
                _objectClassList.EndUpdate();
            }
        }

        private void _moduleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_moduleList.SelectedItems.Count > 0)
            {
                SyncObjectClasses((ModuleViewModel)_moduleList.SelectedItems[0].Tag);
            }
        }

        private void SyncInstances(InstanceListViewModel ocModel)
        {
            System.Diagnostics.Debug.WriteLine("SyncModules");
            _instancesList.BeginUpdate();
            try
            {
                _instancesList.Items.Clear();
                foreach (var dom in ocModel.Instances)
                {
                    var lvi = new ListViewItem(dom.Name) { Tag = dom };
                    dom.PropertyChanged += new PropertyChangedEventHandler(InstancePropertyChanged);
                    _instancesList.Items.Add(lvi);
                }
            }
            finally
            {
                _instancesList.EndUpdate();
            }
        }

        private void _instancesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_instancesList.SelectedItems.Count > 0)
            {
                _dataContextCache.SelectedItem = (DataObjectViewModel)_instancesList.SelectedItems[0].Tag;
            }
            else
            {
                _dataContextCache.SelectedItem = null;
            }
        }

        #region IFormsView Members

        void IFormsView.SetDataContext(INotifyPropertyChanged mdl)
        {
            this.DataContext = (WorkspaceViewModel)mdl;
        }

        internal Renderer Renderer { get; private set; }
        void IFormsView.SetRenderer(Renderer r)
        {
            Renderer = r;
        }

        #endregion

        #region IFormsView Members

        void IFormsView.ShowDialog()
        {
            this.ShowDialog();
        }

        #endregion
    }
}
