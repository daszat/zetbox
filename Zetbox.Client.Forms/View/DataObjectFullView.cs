using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Zetbox.App.GUI;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.Forms.View
{
    public partial class DataObjectFullView : DataObjectFullViewDesignerProxy
    {

        public DataObjectFullView()
        {
            InitializeComponent();
        }

        protected override void OnDataContextChanged()
        {
            base.OnDataContextChanged();
            SyncName();

            ((INotifyCollectionChanged)DataContext.PropertyModels).CollectionChanged += new NotifyCollectionChangedEventHandler(PropertyModels_CollectionChanged);
            SyncProperties();
        }

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnModelPropertyChanged(sender, e);

            if (e.PropertyName == "Name")
                SyncName();
        }

        private void SyncName()
        {
            _objectName.Text = DataContext.Name;
        }

        private void SyncProperties()
        {
            // TODO: much better sync mechanism necessary!
            _propertyPanel.Controls.Clear();
            for (int i = 0; i < DataContext.PropertyModels.Count; i++)
            {
                var containerPanel = new System.Windows.Forms.Panel();
                containerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                containerPanel.Name = String.Format("containerPanel{0}", i);

                _propertyPanel.Controls.Add(containerPanel, 0, i);

                Renderer.ShowModel(DataContext.PropertyModels[i], containerPanel);
            }
        }

        void PropertyModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SyncProperties();
        }


    }

    public class DataObjectFullViewDesignerProxy : FormsUserControl<DataObjectViewModel> { }

}
