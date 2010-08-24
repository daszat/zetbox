using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.CustomControls
{
    public class WindowView : Window
    {
        public WindowView()
        {
            this.Loaded += new RoutedEventHandler(WindowView_Loaded);
        }

        void WindowView_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is WindowViewModel)
            {
                ((WindowViewModel)this.DataContext).PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(WindowView_PropertyChanged);
            }
        }

        void WindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Show" && !((WindowViewModel)this.DataContext).Show) this.Close();
        }
    }
}
