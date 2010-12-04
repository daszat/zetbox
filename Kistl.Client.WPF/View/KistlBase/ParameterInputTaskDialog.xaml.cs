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
using Kistl.Client.WPF.CustomControls;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View.KistlBase
{
    /// <summary>
    /// Interaction logic for ParameterInputTaskDialog.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class ParameterInputTaskDialog : WindowView, IHasViewModel<IValueInputTaskViewModel>
    {
        public ParameterInputTaskDialog()
        {
            InitializeComponent();
        }

        #region IHasViewModel<IValueInputTaskViewModel> Members

        public IValueInputTaskViewModel ViewModel
        {
            get { return (IValueInputTaskViewModel)DataContext; }
        }

        #endregion
    }
}
