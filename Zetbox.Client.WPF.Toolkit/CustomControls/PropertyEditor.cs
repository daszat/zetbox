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


namespace Zetbox.Client.WPF.CustomControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    using System.Windows.Media;
    using System.Windows.Input;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.WPF.Converter;
    using Zetbox.Client.Presentables.ValueViewModels;

    /// <summary>
    /// Defines common (Dependency-)Properties for Controls displaying/editing (Object)Properties
    /// </summary>
    [ContentProperty("Content")]
    public abstract class PropertyEditor : ContentControl
    {
        static PropertyEditor()
        {
            // by default the PropertyEditor itself should not take part 
            // in focus-stuff
            FocusableProperty.OverrideMetadata(typeof(PropertyEditor),
                new FrameworkPropertyMetadata(false));
        }

        public PropertyEditor()
        {
            VerticalContentAlignment = VerticalAlignment.Top;
            MinWidth = 100;

            this.GotFocus += new RoutedEventHandler(PropertyEditor_GotFocus);
            BindingOperations.SetBinding(this, Zetbox.Client.WPF.Styles.Controls.HighlightProperty, new Binding("Highlight") { Mode = BindingMode.OneWay });
        }

        protected abstract FrameworkElement MainControl { get; }

        public new virtual void Focus()
        {
            if (MainControl != null)
            {
                MainControl.Focus();
            }
            else
            {
                base.Focus();
            }
        }

        void PropertyEditor_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TextBox)
            {
                var txt = (TextBox)e.OriginalSource;
                if (txt.AcceptsReturn && txt.MinLines > 1)
                {
                    // Multiline
                    txt.SelectionStart = txt.Text.Length;
                }
                else
                {
                    txt.SelectAll();
                }
            }
        }

        #region Focus Management for BaseValueViewModels

        /// <summary>
        /// Use this method to *properly* implement Focusmanagement for .NET 4.0. This contains hacks to make that work, so nobody else has do pull his/her hair out.
        /// </summary>
        /// <param name="element">the FrameworkElement whose keyboard focus should be managed</param>
        /// <param name="vmGetter">a functor yielding the current BaseValueViewModel whose focus is tied to this view</param>
        protected void SetupFocusManagement(FrameworkElement element, Func<BaseValueViewModel> vmGetter)
        {
            element.GotKeyboardFocus += (s, e) => FocusViewModel(element, vmGetter);
            element.LostKeyboardFocus += (s, e) => BlurViewModel(element);
            // when the control is unbound, DataContextChanged is fired BEFORE LostKeyboardFocus
            // therefore we need to conditionally blur the ViewModel
            element.DataContextChanged += (s, e) => BlurViewModel(element);
        }

        private Dictionary<FrameworkElement, BaseValueViewModel> _focusedModel = new Dictionary<FrameworkElement, BaseValueViewModel>();

        private void FocusViewModel(FrameworkElement element, Func<BaseValueViewModel> vmGetter)
        {
            _focusedModel[element] = vmGetter();
            _focusedModel[element].Focus();
        }

        private void BlurViewModel(FrameworkElement element)
        {
            BaseValueViewModel vm;
            if (_focusedModel.TryGetValue(element, out vm))
            {
                try { vm.Blur(); }
                finally { _focusedModel.Remove(element); }
            }
        }

        #endregion
    }
}
