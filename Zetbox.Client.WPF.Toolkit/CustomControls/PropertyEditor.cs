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

            var b = new Binding("Highlight");
            b.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(this, HighlightProperty, b);
        }


        public Highlight Highlight
        {
            get { return (Highlight)GetValue(HighlightProperty); }
            set { SetValue(HighlightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Highlight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightProperty =
            DependencyProperty.Register("Highlight", typeof(Highlight), typeof(PropertyEditor), new UIPropertyMetadata(null, _OnHighlightChanged));

        private static void _OnHighlightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PropertyEditor editor = (PropertyEditor)d;
            editor.OnHighlightChanged();
        }

        private static readonly BrushConverter _brushConverter = new BrushConverter();
        private static readonly FontStyleConverter _fontStyleConverter = new FontStyleConverter();
        private static readonly FontWeightConverter _fontWeightConverter = new FontWeightConverter();

        private static readonly HighlightGridBackgroundConverter _highlightGridBackgroundConverter = new HighlightGridBackgroundConverter();
        private static readonly HighlightGridForegroundConverter _highlightGridForegroundConverter = new HighlightGridForegroundConverter();
        private static readonly HighlightGridFontStyleConverter _highlightGridFontStyleConverter = new HighlightGridFontStyleConverter();
        private static readonly HighlightGridFontWeightConverter _highlightGridFontWeightConverter = new HighlightGridFontWeightConverter();

        protected virtual void OnHighlightChanged()
        {
            if (MainControl != null)
            {
                SetHighlightValue(MainControl);
            }
        }

        protected virtual void SetHighlightValue(FrameworkElement ctrl)
        {
            if (ctrl != null)
            {
                SetHighlightValue(ctrl, BackgroundProperty, Highlight, _highlightGridBackgroundConverter, _brushConverter);
                SetHighlightValue(ctrl, ForegroundProperty, Highlight, _highlightGridForegroundConverter, _brushConverter);
                SetHighlightValue(ctrl, FontStyleProperty, Highlight, _highlightGridFontStyleConverter, _fontStyleConverter);
                SetHighlightValue(ctrl, FontWeightProperty, Highlight, _highlightGridFontWeightConverter, _fontWeightConverter);
            }
        }

        protected virtual void SetHighlightValue(FrameworkElement ctrl, DependencyProperty dpProp, Highlight h, HighlightConverter converter, TypeConverter finalConverter)
        {
            if (converter == null) throw new ArgumentNullException("converter");
            if (finalConverter == null) throw new ArgumentNullException("finalConverter");

            var value = converter.Convert(h, null, null, null);
            if (value == Binding.DoNothing)
                ctrl.SetValue(dpProp, DependencyProperty.UnsetValue);
            else if (value == null)
                ctrl.SetValue(dpProp, null);
            else if (dpProp.PropertyType.IsAssignableFrom(value.GetType()))
                ctrl.SetValue(dpProp, value);
            else
                ctrl.SetValue(dpProp, finalConverter.ConvertFrom(value));
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
    }
}
