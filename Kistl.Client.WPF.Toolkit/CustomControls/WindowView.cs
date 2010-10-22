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
            this.Closing += new System.ComponentModel.CancelEventHandler(WindowView_Closing);
        }

        void WindowView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WindowViewModel != null)
            {
                e.Cancel = !WindowViewModel.CanClose();
            }
            else
            {
                e.Cancel = false;
            }
        }

        void WindowView_Loaded(object sender, RoutedEventArgs e)
        {
            if (WindowViewModel != null)
            {
                WindowViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(WindowView_PropertyChanged);
            }
        }

        void WindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Show" && !WindowViewModel.Show) this.Close();
        }

        private WindowViewModel WindowViewModel
        {
            get
            {
                return DataContext as WindowViewModel;
            }
        }
    }
}
