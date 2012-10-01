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

namespace Zetbox.Client.WPF.View
{
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
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.WPF.CustomControls;
    using Zetbox.Client.WPF.View.ZetboxBase;

    /// <summary>
    /// Interaction logic for NullableDateTimeValueEditor.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class NullableDateTimeValueEditor : PropertyEditor, IHasViewModel<NullableDateTimePropertyViewModel>
    {
        public NullableDateTimeValueEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            InitializeComponent();

            txtDate.GotKeyboardFocus += (s, e) => ViewModel.Focus();
            txtDate.LostKeyboardFocus += (s, e) => ViewModel.Blur();
            txtTime.GotKeyboardFocus += (s, e) => ViewModel.Focus();
            txtTime.LostKeyboardFocus += (s, e) => ViewModel.Blur();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                FixTimePart();
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DatePartVisible")
            {
                FixTimePart();
            }
        }

        private void FixTimePart()
        {
            // Fix lonely Time Textbox
            txtTime.SetValue(DockPanel.DockProperty, ViewModel.DatePartVisible ? Dock.Right : Dock.Top);
        }

        #region IHasViewModel<NullableDateTimePropertyViewModel> Members

        public NullableDateTimePropertyViewModel ViewModel
        {
            get { return (NullableDateTimePropertyViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get
            {
                if (ViewModel == null) return null;
                return ViewModel.DatePartVisible ? (FrameworkElement)txtDate : (FrameworkElement)txtTime;
            }
        }

        // The DatePicker handles the Enter-KeyDown event, but we have to bubble it to our consumers, 
        // because this will trigger the RefreshCommand (or accept action) in filters (and dialogs)
        private bool _isEnterPressed = false;
        private KeyboardDevice _lastKeyboardDevice = null;
        private PresentationSource _lastInputSource = null;
        private int _lastTimestamp = -1;
        private void txtDate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _isEnterPressed = true;
                _lastKeyboardDevice = e.KeyboardDevice;
                _lastInputSource = e.InputSource;
                _lastTimestamp = e.Timestamp;
            }
        }

        private void txtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isEnterPressed)
            {
                _isEnterPressed = false;

                var args = new KeyEventArgs(_lastKeyboardDevice, _lastInputSource, _lastTimestamp, Key.Enter)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                };

                _lastKeyboardDevice = null;
                _lastInputSource = null;
                _lastTimestamp = -1;

                this.RaiseEvent(args);
            }
        }
    }
}
