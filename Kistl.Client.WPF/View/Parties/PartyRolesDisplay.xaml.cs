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
using Kistl.Client.GUI;
using Kistl.Client.Presentables.Parties;

namespace Kistl.Client.WPF.View.Parties
{
    /// <summary>
    /// Interaction logic for PartyRolesDisplay.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class PartyRolesDisplay : UserControl, IHasViewModel<PartyRolesViewModel>
    {
        public PartyRolesDisplay()
        {
            InitializeComponent();
        }

        public PartyRolesViewModel ViewModel
        {
            get { return (PartyRolesViewModel)DataContext; }
        }
    }
}
