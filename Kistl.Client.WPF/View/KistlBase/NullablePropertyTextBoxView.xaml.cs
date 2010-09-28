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

using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.WPF.View.KistlBase;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for NullablePropertyTextBoxView.xaml
    /// </summary>
    public partial class NullablePropertyTextBoxView : PropertyEditor, IHasViewModel<IValueModel<string>>, IHasViewModel<IValueModelAsString>
    {
        public NullablePropertyTextBoxView()
        {
            InitializeComponent();
        }

        #region IHasViewModel<IValueModelAsString> Members

        public IValueModel<string> ViewModel
        {
            get { return (IValueModel<string>)DataContext; }
        }

        #endregion

        #region IHasViewModel<IValueModelAsString> Members

        IValueModelAsString IHasViewModel<IValueModelAsString>.ViewModel
        {
            get { return (IValueModelAsString)DataContext; }
        }

        #endregion

        /// <summary>
        /// Foreces a manual refresh, as WPF ignores notifications on UpdateSource.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var binding = BindingOperations.GetBindingExpressionBase(txt, TextBox.TextProperty);
            binding.UpdateSource();
            binding.UpdateTarget();
        }
    }
}
