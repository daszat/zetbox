﻿// This file is part of zetbox.
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
using System.Threading.Tasks;
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
using Zetbox.Client.Models;
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
    public class ObjectReferenceDropdownEditor : PropertyEditor, IHasViewModel<ObjectReferenceViewModel>, IDragDropTarget, IDragDropSource
    {
        static ObjectReferenceDropdownEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ObjectReferenceDropdownEditor), new FrameworkPropertyMetadata(typeof(ObjectReferenceDropdownEditor)));
        }

        public ObjectReferenceDropdownEditor()
        {
            _dragDrop = new WpfDragDropHelper(this);
        }

        private WpfDragDropHelper _dragDrop;
        private ComboBox cbValue;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            cbValue = (ComboBox)GetTemplateChild("PART_cbValue");
            cbValue.KeyDown += cbValue_KeyDown;

            var imageBorder = (Border)GetTemplateChild("PART_ImageBorder");
            imageBorder.MouseLeftButtonDown += Border_Click;
        }

        #region IHasViewModel<ObjectReferenceViewModel> Members

        public ObjectReferenceViewModel ViewModel
        {
            get { return (ObjectReferenceViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return (ComboBox)GetTemplateChild("PART_cbValue"); }
        }

        private void cbValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                ViewModel.ResetPossibleValues();
                cbValue.IsDropDownOpen = true;
            }
        }

        private void Border_Click(object sender, RoutedEventArgs e)
        {
            cbValue.Focus();
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

        Task<bool> IDragDropTarget.OnDrop(string format, object data)
        {
            if (ViewModel == null) return Task.FromResult(false);
            return ViewModel.OnDrop(data);
        }

        object IDragDropSource.GetData()
        {
            return ViewModel.DoDragDrop();
        }
        #endregion
    }
}
