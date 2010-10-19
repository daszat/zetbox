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
using Kistl.Client.Presentables.FilterViewModels;

namespace Kistl.Client.WPF.View.Filters
{
    /// <summary>
    /// Interaction logic for StringFilterExpression.xaml
    /// </summary>
    [ViewDescriptor("GUI", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.RangeFilterKind")]
    public partial class RangeFilterEditor : UserControl, IHasViewModel<RangeFilterViewModel>
    {
        public RangeFilterEditor()
        {
            InitializeComponent();
        }

        public RangeFilterViewModel ViewModel
        {
            get { return (RangeFilterViewModel)DataContext; }
        }
    }
}
