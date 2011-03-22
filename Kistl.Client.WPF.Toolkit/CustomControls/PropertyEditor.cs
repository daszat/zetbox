

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
