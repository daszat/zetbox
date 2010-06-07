
namespace Kistl.Client.WPF.View.KistlBase
{
    using System;
    using System.Collections.Generic;
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

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.Client.Presentables;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables.KistlBase;

    /// <summary>
    /// Shows all instances of a given DataTypeModel
    /// </summary>
    [ViewDescriptor("KistlBase", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.InstanceListKind")]
    public partial class InstanceListDisplay
        : UserControl, IHasViewModel<InstanceListViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the ObjectClassDisplay class.
        /// </summary>
        public InstanceListDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens a new WorkspaceModel in its default view with the double clicked item opened.
        /// </summary>
        /// <param name="sender">the sender of this event, a <see cref="ListViewItem"/> is expected</param>
        /// <param name="e">the arguments of this event</param>
        private void ObjectList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListViewItem;
            if (listItem == null)
            {
                return;
            }

            var dataObject = listItem.Content as DataObjectModel;
            if (dataObject == null)
            {
                return;
            }

            // only react to left mouse button double clicks
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            ViewModel.OnItemsDefaultAction(new DataObjectModel[] { dataObject });
            e.Handled = true;
        }

        private void ClassList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.RemovedItems.ForEach<DataObjectModel>(i => ViewModel.SelectedItems.Remove(i));            
            e.AddedItems.ForEach<DataObjectModel>(i => ViewModel.SelectedItems.Add(i));
        }

        #region IHasViewModel<DataTypeModel> Members

        public InstanceListViewModel ViewModel
        {
            get { return (InstanceListViewModel)DataContext; }
        }

        #endregion
    }
}
