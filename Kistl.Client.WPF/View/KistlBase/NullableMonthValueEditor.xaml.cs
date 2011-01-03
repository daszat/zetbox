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
using Kistl.Client.WPF.View.KistlBase;
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for NullableMonthValueEditor.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class NullableMonthValueEditor : PropertyEditor, IHasViewModel<NullableMonthPropertyViewModel>
    {
        public NullableMonthValueEditor()
        {
            InitializeComponent();
        }

        #region IHasViewModel<NullableMonthPropertyViewModel> Members

        public NullableMonthPropertyViewModel ViewModel
        {
            get { return (NullableMonthPropertyViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return cbMonth; }
        }
    }
}
