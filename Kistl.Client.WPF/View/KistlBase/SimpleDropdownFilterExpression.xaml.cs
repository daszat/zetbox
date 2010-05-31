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
    /// Interaction logic for SimpleDropdownFilterExpression.xaml
    /// </summary>
    [ViewDescriptor("GUI", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.SimpleDropdownFilterKind")]
    public partial class SimpleDropdownFilterExpression : UserControl, IHasViewModel<IListTypeFilterViewModel>
    {
        public SimpleDropdownFilterExpression()
        {
            InitializeComponent();
        }

        #region IHasViewModel<IListTypeFilterViewModel> Members

        public IListTypeFilterViewModel ViewModel
        {
            get { return (IListTypeFilterViewModel)DataContext; }
        }

        #endregion
    }
}
