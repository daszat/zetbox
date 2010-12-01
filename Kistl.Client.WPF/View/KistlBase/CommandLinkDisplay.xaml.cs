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
using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.Client.WPF.View.KistlBase
{
    /// <summary>
    /// Interaction logic for ActionView.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class CommandLinkDisplay : UserControl, IHasViewModel<CommandViewModel>
    {
        public CommandLinkDisplay()
        {
            InitializeComponent();
        }

        public CommandViewModel ViewModel
        {
            get { return (CommandViewModel)DataContext; }
        }
    }
}
