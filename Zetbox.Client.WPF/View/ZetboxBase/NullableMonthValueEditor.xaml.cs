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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.WPF.View.ZetboxBase;

namespace Zetbox.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for NullableMonthValueEditor.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class NullableMonthValueEditor : PropertyEditor, IHasViewModel<NullableMonthPropertyViewModel>
    {
        public NullableMonthValueEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
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
