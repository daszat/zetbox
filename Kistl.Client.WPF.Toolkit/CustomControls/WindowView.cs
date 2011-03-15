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

        private bool _closing;
        void WindowView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WindowViewModel != null && WindowViewModel.Show)
            {
                _closing = true;
                // we're coming from WPF, e.g. clicking the red X or calling Close()
                // now we need to tell the view model that it should shut down
                WindowViewModel.Show = false;

                // the ViewModel might want to ignore our signal
                // then we should not close
                if (WindowViewModel.Show)
                {
                    e.Cancel = true;
                    _closing = false;
                }
            }
            else
            {
                // either we do not have a view model, or the view model is alread in shut down
                // both mean that we just have to close
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
            if (e.PropertyName == "Show" && !_closing && !WindowViewModel.Show) this.Close();
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
