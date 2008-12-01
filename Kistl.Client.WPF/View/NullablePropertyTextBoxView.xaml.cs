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
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for NullablePropertyTextBoxView.xaml
    /// </summary>
    public partial class NullablePropertyTextBoxView : UserControl, IView
    {
        public NullablePropertyTextBoxView()
        {
            InitializeComponent();
        }

        private void ClearValueHandler(object sender, RoutedEventArgs e)
        {
            var mdl = (IClearableValue)DataContext;
            mdl.ClearValue();
        }

        #region IView Members

        public void SetModel(PresentableModel mdl)
        {
            DataContext = mdl;
        }

        #endregion
    }
}
