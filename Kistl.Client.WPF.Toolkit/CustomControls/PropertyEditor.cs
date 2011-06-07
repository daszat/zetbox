

namespace Kistl.Client.WPF.CustomControls
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
    using Kistl.Client.Presentables;
    
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

        protected virtual void OnHighlightChanged() 
        {
            if (MainControl != null)
            {
                SetHighlightValue(MainControl, BackgroundProperty, Highlight, "HighlightGridBackgroundConverter");
                SetHighlightValue(MainControl, ForegroundProperty, Highlight, "HighlightGridForegroundConverter");
                SetHighlightValue(MainControl, FontStyleProperty, Highlight, "HighlightGridFontStyleConverter");
                SetHighlightValue(MainControl, FontWeightProperty, Highlight, "HighlightGridFontWeightConverter");
            }
        }

        private static void SetHighlightValue(FrameworkElement ctrl, DependencyProperty dpProp, Highlight h, string converter)
        {
            var value = ((IValueConverter)Application.Current.Resources[converter]).Convert(h, null, null, null);
            if (value == Binding.DoNothing)
                ctrl.SetValue(dpProp, DependencyProperty.UnsetValue);
            else
                ctrl.SetValue(dpProp, value);
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
