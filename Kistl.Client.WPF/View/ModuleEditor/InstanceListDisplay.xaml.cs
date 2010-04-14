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
using Kistl.API.Client;
using ObjectEditorWorkspace = Kistl.Client.Presentables.ObjectEditor.WorkspaceViewModel;
using Kistl.Client.GUI;
using Kistl.Client.Presentables.ModuleEditor;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View.ModuleEditor
{
    /// <summary>
    /// Interaction logic for InstanceListDisplay.xaml
    /// </summary>
    public partial class InstanceListDisplay : UserControl, IHasViewModel<InstanceListViewModel>
    {
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

            var factory = App.Current.AppContext.Factory;
            var newWorkspace = factory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(KistlContext.GetContext());
            newWorkspace.ShowForeignModel(dataObject);
            factory.ShowModel(newWorkspace, true);
            e.Handled = true;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var factory = App.Current.AppContext.Factory;
            var newWorkspace = factory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(KistlContext.GetContext());
            foreach (var item in InstancesList.SelectedItems.OfType<DataObjectModel>())
            {
                newWorkspace.ShowForeignModel(item);
            }
            factory.ShowModel(newWorkspace, true);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ReloadInstances();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            var factory = App.Current.AppContext.Factory;
            var newCtx = KistlContext.GetContext();
            var newWorkspace = factory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
            newWorkspace.ShowForeignModel((DataObjectModel)factory.CreateDefaultModel(newCtx, newCtx.Create(ViewModel.InterfaceType)));
            factory.ShowModel(newWorkspace, true);
        }

        #region IHasViewModel<ModuleEditorDashboardModel> Members

        public InstanceListViewModel ViewModel
        {
            get { return (InstanceListViewModel)DataContext; }
        }

        #endregion
    }
}
