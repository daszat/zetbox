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
using System.Windows.Shapes;

using Kistl.Client.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for KistlDebuggerView.xaml
    /// </summary>
    public partial class KistlDebuggerView : Window, IView
    {
        public KistlDebuggerView()
        {
            InitializeComponent();
        }

        #region IView Members

        public void SetModel(PresentableModel mdl)
        {
            DataContext = (KistlDebuggerAsModel)mdl;
        }

        #endregion
    }
}
