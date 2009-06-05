
namespace Kistl.Client.WPF.View
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

    /// <summary>
    /// Interaction logic for WorkspaceDisplay.xaml, a read-only display of a <see cref="Kistl.Client.Presentables.WorkspaceModel"/>.
    /// </summary>
    public partial class WorkspaceDisplay : Window, Kistl.Client.GUI.IView
    {
        /// <summary>
        /// Initializes a new instance of the WorkspaceDisplay class.
        /// </summary>
        public WorkspaceDisplay()
        {
            InitializeComponent();
        }

        private void ModuleTreeSelectedItemChangedHandler(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var workspaceModel = Model;
            var item = ModuleTree.SelectedItem as Kistl.Client.Presentables.DataObjectModel;
            if (item != null)
            {
                workspaceModel.SelectedItem = item;
            }

        }

        #region IView Members

        /// <summary>
        /// Gets or sets the displayed model. Uses the DataContext as backing store.
        /// </summary>
        private Kistl.Client.Presentables.WorkspaceModel Model
        {
            get { return (Kistl.Client.Presentables.WorkspaceModel)DataContext; }
            set { DataContext = value; }
        }

        /// <summary>
        /// Sets the model to display of this View.
        /// </summary>
        /// <param name="mdl">the model to display</param>
        public void SetModel(Kistl.Client.Presentables.PresentableModel mdl)
        {
            Model = (Kistl.Client.Presentables.WorkspaceModel)mdl;
        }

        #endregion
    }
}
