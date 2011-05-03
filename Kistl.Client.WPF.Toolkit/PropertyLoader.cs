using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Client;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.Toolkit
{
    public class PropertyLoader : IPropertyLoader
    {
        private ViewModel _displayer;
        private Action _loadAction;

        public PropertyLoader(ViewModel displayer, Action loadAction)
        {
            if (displayer == null) throw new ArgumentNullException("displayer");
            if (loadAction == null) throw new ArgumentNullException("loadAction");

            _displayer = displayer;
            _loadAction = loadAction;
        }

        public void Reload()
        {
            _displayer.IsBusy = true;
            System.Windows.Threading.Dispatcher.FromThread(System.Threading.Thread.CurrentThread).BeginInvoke(new Action(() =>
            {
                try
                {
                    _loadAction();
                }
                finally
                {
                    _displayer.IsBusy = false;
                }
            }), System.Windows.Threading.DispatcherPriority.Background); // prio must be Background to let fakeprogressoverlay be rendered
        }
    }
}
