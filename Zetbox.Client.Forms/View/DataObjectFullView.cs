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

                //Renderer.ShowModel(DataContext.PropertyModels[i], containerPanel);
            }
        }

        void PropertyModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SyncProperties();
        }


    }

    public class DataObjectFullViewDesignerProxy : FormsUserControl<DataObjectViewModel> { }

}
