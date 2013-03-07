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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables.TestModule;

namespace Zetbox.Client.WPF.View.TestModule
{
    /// <summary>
    /// Interaction logic for NextEventsTestNavScreenDisplay.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class NextEventsTestNavScreenDisplay : UserControl, IHasViewModel<NextEventsTestNavScreenViewModel>
    {
        public NextEventsTestNavScreenDisplay()
        {
            InitializeComponent();
        }

        public NextEventsTestNavScreenViewModel ViewModel
        {
            get { return (NextEventsTestNavScreenViewModel)DataContext; }
        }
    }
}
