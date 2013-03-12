// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
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
using Zetbox.Client.WPF.Toolkit;
using Zetbox.Client.WPF.View.ZetboxBase;

namespace Zetbox.Client.WPF.View
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Password property is not a dependency property. See here (http://stackoverflow.com/questions/1483892/wpf-binding-to-the-passwordbox-in-mvvm-working-solution) why.
    /// Sadly, we rely on MVVM, so ther's no other way.
    /// </remarks>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class NullablePasswordTextBoxView : PropertyEditor, IHasViewModel<IValueViewModel<string>>, IHasViewModel<IFormattedValueViewModel>
    {
        public NullablePasswordTextBoxView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
            txt.PasswordChanged += new RoutedEventHandler(txt_PasswordChanged);
        }

        #region IHasViewModel<IValueModelAsString> Members

        public IValueViewModel<string> ViewModel
        {
            get { return (IValueViewModel<string>)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion

        #region IHasViewModel<IValueModelAsString> Members

        IFormattedValueViewModel IHasViewModel<IFormattedValueViewModel>.ViewModel
        {
            get { return (IFormattedValueViewModel)WPFHelper.SanitizeDataContext(DataContext); }
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
