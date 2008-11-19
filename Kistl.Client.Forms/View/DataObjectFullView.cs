using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kistl.Client.Presentables;

namespace Kistl.Client.Forms.View
{
    public partial class DataObjectFullView : UserControl
    {
        private DataObjectModel _dataContextCache;
        public DataObjectModel DataContext
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
                
                _dataContextCache.PropertyChanged += new PropertyChangedEventHandler(_dataContextCache_PropertyChanged);
                SyncName();

                _dataContextCache.PropertyModels.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(PropertyModels_CollectionChanged);
                SyncProperties();
            }
        }

        void _dataContextCache_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
                SyncName();
        }

        private void SyncName()
        {
            _objectName.Text = _dataContextCache.Name;
        }

        private void SyncProperties()
        {
            label1.Text = String.Format("Should display {0} properties", _dataContextCache.PropertyModels.Count);

        }

        void PropertyModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SyncProperties();
        }

        public DataObjectFullView()
        {
            InitializeComponent();
        }
    }
}
