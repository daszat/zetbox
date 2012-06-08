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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    /// <summary>
    /// Interaction logic for ActionView.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class CommandLinkDisplay : UserControl, IHasViewModel<CommandViewModel>
    {
        public CommandLinkDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public CommandViewModel ViewModel
        {
            get { return (CommandViewModel)DataContext; }
        }
    }
}
