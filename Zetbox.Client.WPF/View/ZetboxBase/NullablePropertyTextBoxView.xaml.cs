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
    /// Interaction logic for NullablePropertyTextBoxView.xaml
    /// </summary>
    public partial class NullablePropertyTextBoxView : PropertyEditor, IHasViewModel<BaseValueViewModel>, IHasViewModel<IFormattedValueViewModel>
    {
        public NullablePropertyTextBoxView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();

            txtNullablePropertyTextBoxView.GotKeyboardFocus += (s, e) => ViewModel.Focus();
            txtNullablePropertyTextBoxView.LostKeyboardFocus += (s, e) => ViewModel.Blur();
        }

        #region IHasViewModel<BaseValueViewModel> Members

        public BaseValueViewModel ViewModel
        {
            get { return (BaseValueViewModel)DataContext; }
        }

        #endregion

        #region IHasViewModel<IFormattedValueViewModel> Members

        IFormattedValueViewModel IHasViewModel<IFormattedValueViewModel>.ViewModel
        {
            get { return (IFormattedValueViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return txtNullablePropertyTextBoxView; }
        }
    }
}
