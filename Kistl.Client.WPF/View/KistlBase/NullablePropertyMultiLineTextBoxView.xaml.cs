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
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.Client.WPF.CustomControls;
using Kistl.Client.WPF.View.KistlBase;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for NullablePropertyMultiLineTextBoxView.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class NullablePropertyMultiLineTextBoxView : PropertyEditor, IHasViewModel<IValueViewModel<string>>
    {
        public NullablePropertyMultiLineTextBoxView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }


        #region IHasViewModel<IValueModel<string>> Members

        public IValueViewModel<string> ViewModel
        {
            get { return (IValueViewModel<string>)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return txt; }
        }
    }
}
