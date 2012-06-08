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
using Kistl.Client.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View.KistlBase
{
    /// <summary>
    /// Interaction logic for DataObjectLineDisplay.xaml
    /// </summary>
    public partial class DataObjectLineDisplay : UserControl, IHasViewModel<DataObjectViewModel>
    {
        public DataObjectLineDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public DataObjectViewModel ViewModel
        {
            get { return (DataObjectViewModel)DataContext; }
        }
    }
}
