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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.WPF.CustomControls;

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    /// <summary>
    /// Interaction logic for ParameterInputTaskDialog.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
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
