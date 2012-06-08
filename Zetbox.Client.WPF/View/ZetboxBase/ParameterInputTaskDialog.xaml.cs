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
using System.Windows.Shapes;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.WPF.CustomControls;

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
            if (DesignerProperties.GetIsInDesignMode(this)) return;
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
