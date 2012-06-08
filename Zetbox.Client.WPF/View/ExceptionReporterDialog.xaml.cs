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
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for ExceptionReporterDialog.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class ExceptionReporterDialog : WindowView, IHasViewModel<ExceptionReporterViewModel>
    {
        public ExceptionReporterDialog()
        {
            InitializeComponent();
        }

        public ExceptionReporterViewModel ViewModel
        {
            get { return (ExceptionReporterViewModel)DataContext; }
        }
    }
}
