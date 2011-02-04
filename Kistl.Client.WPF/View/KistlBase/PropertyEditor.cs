using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

using Kistl.GUI;
using Kistl.GUI.Renderer.WPF.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace Kistl.Client.WPF.View.KistlBase
{
    /// <summary>
    /// Defines common (Dependency-)Properties for Controls displaying/editing (Object)Properties
    /// </summary>
    [ContentProperty("Content")]
    public abstract class PropertyEditor : UserControl
    {
        public PropertyEditor()
        {
            VerticalContentAlignment = VerticalAlignment.Top;
            MinWidth = 100;

            this.Loaded += new RoutedEventHandler(PropertyEditor_Loaded);
            this.GotFocus += new RoutedEventHandler(PropertyEditor_GotFocus);
        }

        void PropertyEditor_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TextBox)
            {
                ((TextBox)e.OriginalSource).SelectAll();
            }
        }

        void PropertyEditor_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeFocusManager();            
        }

        protected void InitializeFocusManager()
        {
            if (MainControl != null)
            {
                this.SetValue(FocusManager.FocusedElementProperty, MainControl);
            }
        }

        protected abstract FrameworkElement MainControl { get; }
    }
}
