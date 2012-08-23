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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using Zetbox.API.Utils;
using Zetbox.Client.Presentables;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.CustomControls
{
    public class WindowView : Window
    {
        private Stopwatch _watch;
        public WindowView()
        {
            _watch = new Stopwatch();
            _watch.Start();
            this.Initialized += (s,e) => {
                Debug.WriteLine(string.Format("Window: Initialised after {0:N2}ms", _watch.ElapsedMilliseconds));
            };
            this.Loaded += new RoutedEventHandler(WindowView_Loaded);
            Debug.WriteLine("Window: Constructed");
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
            Debug.WriteLine(string.Format("Window: Loaded after {0:N2}ms", _watch.ElapsedMilliseconds));
            _watch.Stop();
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

                if (WindowViewModel != null) this.ApplyIsBusyBehaviour(WindowViewModel);
            }
        }
    }
}
