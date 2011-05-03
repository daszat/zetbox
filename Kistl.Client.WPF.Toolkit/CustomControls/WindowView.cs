using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using Kistl.API.Utils;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.CustomControls
{
    public class WindowView : Window
    {
        public WindowView()
        {
            this.Loaded += new RoutedEventHandler(WindowView_Loaded);
        }

        private bool _closing;
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
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
                WindowViewModel.PropertyChanged += WindowView_PropertyChanged;
            }
            this.Activate();
        }

        void WindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Show":
                    if (!_closing && !WindowViewModel.Show) this.Close();
                    break;
                case "IsBusy":
                    var child = LogicalTreeHelper.GetChildren(this).OfType<FrameworkElement>().FirstOrDefault();
                    if (child != null)
                        if (WindowViewModel.IsBusy)
                        {
                            ContentAdorner.ShowWaitDialog(child);
                        }
                        else
                        {
                            ContentAdorner.HideWaitDialog(child);
                        }
                    break;
            }
        }

        private WindowViewModel WindowViewModel
        {
            get
            {
                return DataContext as WindowViewModel;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == DataContextProperty)
            {
                var inpc = e.OldValue as INotifyPropertyChanged;
                if (inpc != null)
                {
                    inpc.PropertyChanged -= WindowView_PropertyChanged;
                }

                inpc = e.NewValue as INotifyPropertyChanged;
                if (inpc != null)
                {
                    inpc.PropertyChanged += WindowView_PropertyChanged;
                }
            }
        }
    }
}
