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

namespace Kistl.Client.WPF.View.GridCells
{
    /// <summary>
    /// Interaction logic for ReferenceEditor.xaml
    /// </summary>
    public partial class ReferenceEditor : UserControl
    {
        public ReferenceEditor()
        {
            InitializeComponent();
        }

        private void ClearValueHandler(object sender, RoutedEventArgs e)
        {
            ((ObjectReferenceModel)DataContext).Value = null;
        }
    }
}
