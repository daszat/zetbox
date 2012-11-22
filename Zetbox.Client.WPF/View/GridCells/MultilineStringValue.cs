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
using Zetbox.App.GUI;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.WPF.Toolkit;


namespace Zetbox.Client.WPF.View.GridCells
{
    /// <summary>
    /// Interaction logic for StringValue.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public class MultilineStringValue : UserControl, IHasViewModel<MultiLineStringValueViewModel>
    {
        static MultilineStringValue()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultilineStringValue), new FrameworkPropertyMetadata(typeof(MultilineStringValue)));

        }

        #region IHasViewModel<MultiLineStringPropertyModel> Members

        public MultiLineStringValueViewModel ViewModel
        {
            get { return (MultiLineStringValueViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion
    }
}
