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

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    /// <summary>
    /// Interaction logic for ObjectReferenceEditor.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class ObjectReferenceEditor : PropertyEditor, IHasViewModel<ObjectReferenceViewModel>, IDragDropTarget, IDragDropSource
    {
        static ObjectReferenceEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ObjectReferenceEditor), new FrameworkPropertyMetadata(typeof(ObjectReferenceEditor)));
        }

        public ObjectReferenceEditor()
        {
            _dragDrop = new WpfDragDropHelper(this);
        }

        private WpfDragDropHelper _dragDrop;

        #region IHasViewModel<ObjectReferenceModel> Members

        public ObjectReferenceViewModel ViewModel
        {
            get { return (ObjectReferenceViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return null; }
        }

        #region IDragDrop*
        bool IDragDropTarget.CanDrop
        {
            get { return ViewModel != null && ViewModel.CanDrop; }
        }

        string[] IDragDropTarget.AcceptableDataFormats
        {
            get
            {
                // Support as may known things as possible
                // A speciallized ViewModel may handle the data
                return WpfDragDropHelper.AllAcceptableDataFormats;
            }
        }

        bool IDragDropTarget.OnDrop(string format, object data)
        {
            if (ViewModel == null) return false;
            return ViewModel.OnDrop(data);
        }
        object IDragDropSource.GetData()
        {
            return ViewModel.DoDragDrop();
        }
        #endregion
    }
}
