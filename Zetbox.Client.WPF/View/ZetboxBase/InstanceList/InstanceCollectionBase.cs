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

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.Base;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.Client.WPF.Toolkit;
    using Zetbox.Client.WPF.CustomControls;

    public abstract class InstanceCollectionBase : UserControl, IHasViewModel<InstanceListViewModel>
    {
        public InstanceCollectionBase()
        {
        }        

        #region ItemActivatedHandler
        /// <summary>
        /// Opens a new WorkspaceModel in its default view with the double clicked item opened.
        /// </summary>
        /// <param name="sender">the sender of this event, a <see cref="ListViewItem"/> is expected</param>
        /// <param name="e">the arguments of this event</param>
        protected void ItemActivatedHandler(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null && ViewModel.SelectedItem != null)
            {
                ViewModel.ExecItemsDefaultAction();
            }

            e.Handled = true;
        }
        #endregion

        #region Sorting management

        protected abstract void SetHeaderTemplate(DependencyObject header, DataTemplate template);
        private WpfSortHelper _sortHelper;
        protected WpfSortHelper SortHelper
        {
            get
            {
                if (_sortHelper == null)
                {
                    _sortHelper = new WpfSortHelper(this, ViewModel, SetHeaderTemplate);
                }
                return _sortHelper;
            }
        }
        
        #endregion

        protected void RefreshCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.ReloadInstances();
        }

        #region IHasViewModel<InstanceListViewModel> Members

        public InstanceListViewModel ViewModel
        {
            get { return (InstanceListViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion
    }
}
