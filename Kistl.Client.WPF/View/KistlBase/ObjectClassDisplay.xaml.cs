
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

    using Kistl.API.Client;
    using Kistl.Client.Presentables;

    /// <summary>
    /// Interaction logic for ObjectClassDisplay.xaml
    /// </summary>
    public partial class ObjectClassDisplay
        : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the ObjectClassDisplay class.
        /// </summary>
        public ObjectClassDisplay()
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

            var factory = App.Current.AppContext.Factory;
            var newWorkspace = factory.CreateSpecificModel<WorkspaceModel>(KistlContext.GetContext());
            newWorkspace.ShowForeignModel(dataObject);
            factory.ShowModel(newWorkspace, true);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var factory = App.Current.AppContext.Factory;
            var newWorkspace = factory.CreateSpecificModel<WorkspaceModel>(KistlContext.GetContext());
            foreach (var item in ClassList.SelectedItems.OfType<DataObjectModel>())
            {
                newWorkspace.ShowForeignModel(item);
            }
            factory.ShowModel(newWorkspace, true);
        }
    }
}
