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
using Kistl.Client.Presentables.KistlBase;

namespace Kistl.Client.WPF.View.KistlBase
{
    /// <summary>
    /// Interaction logic for BoolFilterExpression.xaml
    /// </summary>
    [ViewDescriptor("GUI", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.SimpleBoolFilterKind")]
    public partial class BoolFilterExpression : UserControl, IHasViewModel<IValueTypeFilterViewModel<bool>>
    {
        public BoolFilterExpression()
        {
            InitializeComponent();
        }

        public IValueTypeFilterViewModel<bool> ViewModel
        {
            get { return (IValueTypeFilterViewModel<bool>)DataContext; }
        }
    }
}
