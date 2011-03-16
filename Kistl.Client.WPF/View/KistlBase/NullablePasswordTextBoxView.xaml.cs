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
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.Client.WPF.CustomControls;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Password property is not a dependency property. See here (http://stackoverflow.com/questions/1483892/wpf-binding-to-the-passwordbox-in-mvvm-working-solution) why.
    /// Sadly, we rely on MVVM, so ther's no other way.
    /// </remarks>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class NullablePasswordTextBoxView : PropertyEditor, IHasViewModel<IValueViewModel<string>>, IHasViewModel<IFormattedValueViewModel>
    {
        public NullablePasswordTextBoxView()
        {
            InitializeComponent();
            txt.PasswordChanged += new RoutedEventHandler(txt_PasswordChanged);
        }

        #region IHasViewModel<IValueModelAsString> Members

        public IValueViewModel<string> ViewModel
        {
            get { return (IValueViewModel<string>)DataContext; }
        }

        #endregion

        #region IHasViewModel<IValueModelAsString> Members

        IFormattedValueViewModel IHasViewModel<IFormattedValueViewModel>.ViewModel
        {
            get { return (IFormattedValueViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return txt; }
        }

        void txt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Changed every keystroke
            if (ViewModel != null) ViewModel.Value = txt.Password;
        }
    }
}
