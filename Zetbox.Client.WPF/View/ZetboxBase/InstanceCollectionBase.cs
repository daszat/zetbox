
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
                ViewModel.OnItemsDefaultAction(new DataObjectViewModel[] { ViewModel.SelectedItem });
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
            get { return (InstanceListViewModel)DataContext; }
        }

        #endregion
    }
}
