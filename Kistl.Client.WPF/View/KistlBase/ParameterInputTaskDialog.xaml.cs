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
    [ViewDescriptor("KistlBase", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.ParameterInputTaskDialog")]
    public partial class ParameterInputTaskDialog : WindowView, IHasViewModel<ParameterInputTaskViewModel>
    {
        public ParameterInputTaskDialog()
        {
            InitializeComponent();
        }

        #region IHasViewModel<ParameterInputTaskViewModel> Members

        public ParameterInputTaskViewModel ViewModel
        {
            get { return (ParameterInputTaskViewModel)DataContext;}
        }

        #endregion
    }
}
