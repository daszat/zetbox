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
using Kistl.App.GUI;

using Kistl.Client.Presentables;
using Kistl.Client.GUI;


namespace Kistl.Client.WPF.View.GridCells
{
    /// <summary>
    /// Interaction logic for StringValue.xaml
    /// </summary>
    [ViewDescriptor("GUI", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.MultiLineTextboxGridKind")]
    public partial class MultilineStringValue : UserControl, IHasViewModel<MultiLineStringPropertyModel>
    {
        public MultilineStringValue()
        {
            InitializeComponent();
        }

        #region IHasViewModel<MultiLineStringPropertyModel> Members

        public MultiLineStringPropertyModel ViewModel
        {
            get { return (MultiLineStringPropertyModel)DataContext; }
        }

        #endregion
    }
}
