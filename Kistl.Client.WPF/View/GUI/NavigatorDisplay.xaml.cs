

namespace Kistl.Client.WPF.View.GUI
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
    using System.Windows.Shapes;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables.GUI;

    /// <summary>
    /// Interaction logic for NavigatorDisplay.xaml
    /// </summary>
    [ViewDescriptor("GUI", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.Navigator")]
    public partial class NavigatorDisplay : Window, IHasViewModel<NavigatorViewModel>
    {
        public NavigatorDisplay()
        {
            InitializeComponent();
        }

        #region IHasViewModel<NavigatorViewModel> Members

        public NavigatorViewModel ViewModel
        {
            get { return (NavigatorViewModel)DataContext; }
        }

        #endregion
    }
}
