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
    public partial class DataObjectListView : DataObjectListViewDesignerProxy
    {
        public DataObjectListView()
        {
            InitializeComponent();
        }

        protected override void OnDataContextChanged()
        {
            base.OnDataContextChanged();
            SyncLabel();
        }

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnModelPropertyChanged(sender, e);
            if (e.PropertyName == "Label")
                SyncLabel();
        }

        private void SyncLabel()
        {
            _label.Text = DataContext.Label;
        }
    }

    public class DataObjectListViewDesignerProxy : FormsUserControl<ObjectListModel> { }
}
